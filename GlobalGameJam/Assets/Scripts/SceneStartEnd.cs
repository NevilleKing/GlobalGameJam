using UnityEngine;
using System.Collections;

public class SceneStartEnd : MonoBehaviour {

    private SpriteRenderer fader;
    private bool fadeIn = true;
    private bool isEnabled = true;

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
                    currColour.a -= 0.01f;
                    fader.color = currColour;
                }
                else
                {
                    isEnabled = false;
                    GameObject.Find("Canvas").GetComponent<NodeManager>().enabled = true;
                }
            }
            else
            {
                if (fader.color.a < 255)
                {
                    Color currColour = fader.color;
                    currColour.a += 0.01f;
                    fader.color = currColour;
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
