using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour {

    public bool startsDialogue = false;

    private NodeManager nm;

    void Start()
    {
        nm = GameObject.Find("Canvas").GetComponent<NodeManager>();
    }

    void OnMouseDown()
    {
        if (!nm.dialogueInProgress)
        {
            if (startsDialogue)
                GameObject.Find("Canvas").GetComponent<NodeManager>().unPause();
            Destroy(gameObject);
        }
    }
}
