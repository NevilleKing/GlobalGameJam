using UnityEngine;
using System.Collections;
using System.Xml;

public class NodeManager : MonoBehaviour
{
    
    public DialogueNode head;

    public NodeManager()
    {
        // Create a node and populate the head value
        DialogueNode currentNode = new DialogueNode();
        head = currentNode;

        // Read in the XML file
        XmlTextReader reader = new XmlTextReader("Assets/Dialogue/Dialogue.xml");

        XmlDocument xml = new XmlDocument();
        xml.Load(reader);
        XmlNodeList sections = xml.SelectNodes("/dialogue/dialogueSection");

        foreach (XmlNode dSection in sections)
        {


            currentNode.dialogueText = dSection["text"].InnerText;

            if (dSection["options"] != null)
            {
                XmlNodeList options = dSection["options"].ChildNodes;
                foreach(XmlNode option in options)
                {
                    DialogueNode myOption = new DialogueNode();
                    myOption.dialogueText = option["reply"].InnerText;
                    currentNode.nextNode.Add(myOption);
                }
            }


        }


        //for(int i = 0; i < 5; i++)
        //{
        //    currentNode.dialogueText = "First words. Hooray! " + i;
        //    currentNode.nextNode.Add(new DialogueNode());
        //    currentNode = currentNode.nextNode[0];
        //}

        //currentNode = head;
        //while (currentNode.nextNode.Count != 0)
        //{
        //    Debug.Log(currentNode.dialogueText);
        //    currentNode = currentNode.nextNode[0];
        //}
    }
}
