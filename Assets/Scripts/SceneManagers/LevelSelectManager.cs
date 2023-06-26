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
