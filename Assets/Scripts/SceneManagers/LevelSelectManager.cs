using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : SceneManagerCommon
{
    public Image levelImage;
    public int index;
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
            volumeVar = Mathf.MoveTowards(volumeVar, 1f, fadeInTime * Time.deltaTime);
            if (volumeVar >= 1)
            {
                toFadeIn = false;
            }
            //volumeVar = Mathf.SmoothDamp(volumeVar, 1f, ref volumeRef, fadeInTime - 1f);
            /*if (volumeVar > 0.99f)
            {
                volumeVar = 1f;
                toFadeIn = false;
            }*/
        }
        else if (toFadeOut == true)
        {
            volumeVar = Mathf.MoveTowards(volumeVar, 0f, fadeOutTime * Time.deltaTime);
            if (volumeVar <= 0)
            {
                toFadeOut = false;
            }
        }
    }
    public void ExitClicked()
    {
        if (index == 0)
        {
            ExitScene("TutorialLevel");
        }
        else
        {
            ExitScene("Level" + index);
        }
    }

}
