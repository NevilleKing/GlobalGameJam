using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneStartEnd : MonoBehaviour {

    private SpriteRenderer fader;
    public bool fadeIn = true;
    private bool isEnabled = true;

    public bool startDialgoue = true;

    public int nextScene;

    public float fadeSpeed = 0.01f;

    void Start()
    {
        fader = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isEnabled)
        {
            if (fadeIn)
            {
                if (fader.color.a > 0)
                {
                    Color currColour = fader.color;
                    currColour.a -= fadeSpeed;
                    fader.color = currColour;
                }
                else
                {
                    isEnabled = false;
                    if (startDialgoue)
                        GameObject.Find("Canvas").GetComponent<NodeManager>().enabled = true;
                }
            }
            else
            {
                if (fader.color.a < 1.2f)
                {
                    Color currColour = fader.color;
                    currColour.a += fadeSpeed;
                    fader.color = currColour;
                }
                else
                {
                    SceneManager.LoadScene(nextScene);
                }
            }
        }
    }

    public void FadeOut()
    {
        fadeIn = false;
        isEnabled = true;
    }
}
