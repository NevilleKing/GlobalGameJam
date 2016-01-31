using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void onClick()
    {
        SceneManager.LoadScene(1);
    }
}
