using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{

    public GameObject DialogueSingle;
    public GameObject DialogueMultiple;

    private GameObject canvas;

    private DialogueNode head;

    private GameObject currentSpawnedDialogue;

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
        XmlTextReader reader = new XmlTextReader("Assets/Dialogue/Dialogue.xml");

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

            if (dSection["options"] != null)
            {
                XmlNodeList options = dSection["options"].ChildNodes;
                foreach(XmlNode option in options)
                {
                    DialogueNode myOption = new DialogueNode();
                    myOption.dialogueText = option["text"].InnerText;
                    DialogueNode replyOption = new DialogueNode();
                    replyOption.dialogueText = option["reply"].InnerText;
                    myOption.nextNode.Add(replyOption);
                    currentNode.choiceCount++;
                    currentNode.nextNode.Add(myOption);
                }
            }

            prevNode = currentNode;

        }

        //currentNode = head;
        //while (currentNode.nextNode != null)
        //{
        //    Debug.Log("Text: " + currentNode.dialogueText);
        //    if (currentNode.choiceCount > 1)
        //    {
        //        foreach(DialogueNode n in currentNode.nextNode)
        //        {
        //            Debug.Log("     Option: " + n.dialogueText);
        //            Debug.Log("         Reply: " + n.nextNode[0].dialogueText);
        //        }
        //        currentNode = currentNode.nextNode[0].nextNode[0].nextNode[0];
        //    }
        //    else
        //    {
        //        currentNode = currentNode.nextNode[0];
        //    }
        //}

        NextOption(head);

    }

    void NextOption(DialogueNode nodeToSpawn)
    {
        if (currentSpawnedDialogue != null)
        {
            Destroy(currentSpawnedDialogue);
        }

        if (nodeToSpawn.choiceCount == 1)
        {
            currentSpawnedDialogue = Instantiate(DialogueSingle) as GameObject;
        }
        else
        {
            currentSpawnedDialogue = Instantiate(DialogueMultiple) as GameObject;
        }

        GameObject txt = GameObject.FindGameObjectWithTag("SpeechText");
        txt.GetComponent<Text>().text = nodeToSpawn.dialogueText;
        currentSpawnedDialogue.transform.SetParent(canvas.transform);
        currentSpawnedDialogue.GetComponent<RectTransform>().offsetMax = new Vector2(-200f, 150f);
    }
}
