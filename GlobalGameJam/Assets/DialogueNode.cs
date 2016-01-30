using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DialogueNode
{
    // How long the text will show for
    public float time = 5.0f;

    // The line of text this node is storing
    public string dialogueText;

    public int choiceCount = 1;

    //DialogueNode[] nextNode = new DialogueNode[3];

    public List<DialogueNode> nextNode = new List<DialogueNode>();

}
