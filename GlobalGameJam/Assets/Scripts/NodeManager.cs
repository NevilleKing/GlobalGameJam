using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour
{

    public GameObject DialogueSingle;
    public GameObject DialogueMultiple;

    private GameObject canvas;

    private DialogueNode head;

    private GameObject currentSpawnedDialogue;

    private DialogueNode currentNode;

    public string path = "Assets/Dialogue/Dialogue.xml";

    private float currentTimer = 0.0f;
    private bool timerOn = false;

    public int currentScene;

    public GameObject PlayerDeadAnim;

    void Start()
    {
        canvas = gameObject;
        init();
    }

    private void init()
    {
        // Create a node and populate the head value
        DialogueNode currentNode = new DialogueNode();
        head = currentNode;

        // Read in the XML file
        XmlTextReader reader = new XmlTextReader(path);

        XmlDocument xml = new XmlDocument();
        xml.Load(reader);
        XmlNodeList sections = xml.SelectNodes("/dialogue/dialogueSection");

        DialogueNode prevNode = null;

        foreach (XmlNode dSection in sections)
        {

            if (prevNode != null)
            {
                currentNode = new DialogueNode();
                if (prevNode.nextNode.Count > 0)
                {
                    foreach (DialogueNode n in prevNode.nextNode)
                    {
                        n.nextNode[0].nextNode.Add(currentNode);
                    }
                }
                else
                {
                    prevNode.nextNode.Add(currentNode);
                    prevNode.choiceCount = 1;
                }
            }

            currentNode.dialogueText = dSection["text"].InnerText;

            if (dSection["image"] != null)
               currentNode.image = dSection["image"].InnerText;

            if (dSection["scenechange"] != null)
            {
                currentNode.sceneChange = dSection["scenechange"].InnerText;
            }

            if (dSection["options"] != null)
            {
                XmlNodeList options = dSection["options"].ChildNodes;
                foreach(XmlNode option in options)
                {
                    DialogueNode myOption = new DialogueNode();
                    myOption.dialogueText = option["text"].InnerText;
                    DialogueNode replyOption = new DialogueNode();
                    replyOption.dialogueText = option["reply"].InnerText;
                    if (option["reply"].GetAttribute("action") == "die")
                        replyOption.playerDead = true;
                    if (option["image"] != null)
                        replyOption.image = option["image"].InnerText;
                    myOption.nextNode.Add(replyOption);
                    currentNode.choiceCount++;
                    currentNode.nextNode.Add(myOption);
                }
            }

            prevNode = currentNode;

        }

        NextOption(head);

    }

    void Update()
    {
        if (timerOn)
        {
            if (currentTimer > 0)
            {
                currentTimer -= 0.02f;
            }
            else
            {
                timerOn = false;
                NextOption(currentNode.nextNode[0]);
            }
        }

        GameObject txt = GameObject.FindGameObjectWithTag("SpeechText");
        if (txt != null)
        {
            if (txt.GetComponent<Text>().text == "")
            {
                txt.GetComponent<Text>().text = currentNode.dialogueText;
            }
        }

    }

    void NextOption(DialogueNode nodeToSpawn)
    {
        if (currentNode != null && currentNode.playerDead)
        {
            GameObject deadAnim = Instantiate(PlayerDeadAnim);

            deadAnim.GetComponent<SceneStartEnd>().nextScene = currentScene;

            return;
        }

        currentNode = nodeToSpawn;

        if (currentSpawnedDialogue != null)
        {
            Destroy(currentSpawnedDialogue);
        }

        if (currentNode.nextNode.Count == 0)
        {
            GameObject.Find("Fade").GetComponent<SceneStartEnd>().FadeOut();
            this.enabled = false;
            return;
        }

        if (nodeToSpawn.nextNode.Count == 1 || nodeToSpawn.nextNode.Count == 0)
        {
            currentSpawnedDialogue = Instantiate(DialogueSingle) as GameObject;
            currentTimer = nodeToSpawn.time;
            timerOn = true;
        }
        else
        {
            currentSpawnedDialogue = Instantiate(DialogueMultiple) as GameObject;
            Button[] buttons = currentSpawnedDialogue.GetComponentsInChildren<Button>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponentInChildren<Text>().text = currentNode.nextNode[i].dialogueText;
            }

            buttons[0].onClick.AddListener(delegate () {
                NextOption(currentNode.nextNode[0].nextNode[0]);
            });

            buttons[1].onClick.AddListener(delegate () {
                NextOption(currentNode.nextNode[1].nextNode[0]);
            });

            buttons[2].onClick.AddListener(delegate () {
                NextOption(currentNode.nextNode[2].nextNode[0]);
            });

        }

        if (currentNode.image != null)
        {
            Image img = currentSpawnedDialogue.GetComponentsInChildren<Image>()[1];
            img.sprite = Resources.Load<Sprite>(currentNode.image);
            Color c = img.color;
            c.a = 1.0f;
            img.color = c;
        }

        if (currentNode.sceneChange != null)
        {
            Camera.main.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(currentNode.sceneChange);
        }

        currentSpawnedDialogue.transform.SetParent(canvas.transform);
        currentSpawnedDialogue.GetComponent<RectTransform>().offsetMax = new Vector2(-200f, 150f);
    }
}
