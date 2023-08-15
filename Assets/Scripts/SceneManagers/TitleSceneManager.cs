using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleSceneManager : SceneManagerCommon
{
    public GameObject optionsPanel;
    public TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        //buttonText.text = PlayerPrefs.GetString("wearyStatus", "on");
        blackScreenImage = blackScreenHolder.GetComponent<Image>(); //fade in for transition
        BGMFadeIn();
        BlackScreenFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = PlayerPrefs.GetString("wearyStatus", "on");
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
            //volumeVar = Mathf.SmoothDamp(volumeVar, 0f, ref volumeRef, fadeOutTime - 1.1f);
            /*if (volumeVar < 0f)
            {
                volumeVar = 0f;
                toFadeOut = false;
            }*/
        }
    }

    public void QuitGame()
    {
        StartCoroutine(QuitApplication());
    }

    public void ShowOptionsPanel()
    {
        if (!optionsPanel.activeInHierarchy)
        {
            optionsPanel.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(false);
        }
    }

    public void WearyToggle() //set the weary status to on or off
    {
        if (PlayerPrefs.GetString("wearyStatus", "on") == "on")
        {
            PlayerPrefs.SetString("wearyStatus", "off");
        }
        else
        {
            PlayerPrefs.SetString("wearyStatus", "on");
        }
    }

    IEnumerator QuitApplication()
    {
        BGMFadeOut();
        BlackScreenFadeOut();
        yield return new WaitForSeconds(fadeOutTime);
        Application.Quit();
    }
}
