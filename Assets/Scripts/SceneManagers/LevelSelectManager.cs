using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : SceneManagerCommon
{
    public Image levelImage;
    public Image popupImage;
    //public Image locked;
    public Sprite[] imageSprite;
    public Sprite[] popupSprite;
    public GameObject levelPopup;
    public GameObject lockedHolder;
    public int index = 0;
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
    public void ExitClicked() //To load levels
    {
        if (index == 0)
        {
            ExitScene("FirstLevelCutscene");
        }
        else
        {
            ExitScene("Level" + (index + 1));
        }
    }

    public void SwitchCard() //Since there's only 2 levels, just switch between them according to the index
    {
        if(index == 0)
        {
            if (PlayerPrefs.GetString("tutorialComplete", "false") == "true")
            {
                if (lockedHolder.activeInHierarchy)
                {
                    lockedHolder.SetActive(false);
                }

            }
            else
            {
                lockedHolder.SetActive(true);
            }
            index = 1;
            levelImage.sprite = imageSprite[index]; //change the background sprite
        }
        else
        {
            if (lockedHolder.activeInHierarchy)
            {
                lockedHolder.SetActive(false);
            }
            index = 0;
            levelImage.sprite = imageSprite[index];
        }
    }

    public void MissionClicked()
    {
        levelPopup.SetActive(true);
        popupImage.sprite = popupSprite[index]; //change the background sprite
    }

    public void ReturnSelect() //return to the level select
    {
        levelPopup.SetActive(false);
    }


}
