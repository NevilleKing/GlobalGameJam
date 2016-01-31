using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("bosschathead");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GetComponent<Image>().sprite.ToString());
    }
}
