using UnityEngine;
using System.Collections;

public class NodeManager : MonoBehaviour
{
    
    public DialogueNode head;

    public NodeManager()
    {
        // Create a node and populate the head value
        DialogueNode currentNode = new DialogueNode();
        head = currentNode;

        for(int i = 0; i < 5; i++)
        {
            currentNode.dialogueText = "First words. Hooray! " + i;
            currentNode.nextNode.Add(new DialogueNode());
            currentNode = currentNode.nextNode[0];
        }

        currentNode = head;
        while (currentNode.nextNode.Count != 0)
        {
            Debug.Log(currentNode.dialogueText);
            currentNode = currentNode.nextNode[0];
        }
    }
}
