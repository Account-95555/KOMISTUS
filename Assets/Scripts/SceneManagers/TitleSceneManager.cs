using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : SceneManagerCommon
{
    // Start is called before the first frame update
    void Start()
    {
        blackScreenImage = blackScreenHolder.GetComponent<Image>();
        BGMFadeIn();
        BlackScreenFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        //Volume Fade Code
        BGMSource.volume = volumeVar;
        if (toFadeIn == true)
        {
            volumeVar = Mathf.SmoothDamp(volumeVar, 1f, ref volumeRef, fadeInTime - 1f);
            if (volumeVar > 0.99f)
            {
                volumeVar = 1f;
                toFadeIn = false;
            }
        }
        else if (toFadeOut == true)
        {
            volumeVar = Mathf.SmoothDamp(volumeVar, 0f, ref volumeRef, fadeOutTime - 1f);
            if (volumeVar < 0f)
            {
                volumeVar = 0f;
                toFadeOut = false;
            }
        }
    }
}
