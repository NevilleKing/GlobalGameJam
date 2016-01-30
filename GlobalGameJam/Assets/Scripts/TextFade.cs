using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{

    private Text fader;
    private enum fadeState {FadingIn, Idle, FadingOut};
    private fadeState fadeIn = fadeState.FadingIn;

    public float fadeSpeed = 0.01f;

    private float timer = 5.0f;

    public bool startDialogue = false;

    void Start()
    {
        fader = GetComponent<Text>();
    }

    void Update()
    {
        if (fadeIn == fadeState.FadingIn)
        {
            if (fader.color.a < 1.0)
            {
                Color currColour = fader.color;
                currColour.a += fadeSpeed;
                fader.color = currColour;
            }
            else
            {
                fadeIn = fadeState.Idle;
            }
        }
        else if (fadeIn == fadeState.Idle)
        {
            if (timer < 0)
            {
                fadeIn = fadeState.FadingOut;
            }
            else
            {
                timer -= 0.05f;
            }
        }
        else
        {
            {
                if (fader.color.a > 0)
                {
                    Color currColour = fader.color;
                    currColour.a -= fadeSpeed;
                    fader.color = currColour;
                }
                else
                {
                    if (startDialogue)
                    {
                        GameObject.Find("Canvas").GetComponent<NodeManager>().enabled = true;
                        enabled = false;
                    }
                    else
                    {
                        GameObject.Find("MoreText").GetComponent<TextFade>().enabled = true;
                        enabled = false;
                    }
                }
            }
        }
    }

}
