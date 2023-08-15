using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class CutsceneManager : SceneManagerCommon
{
    public GameObject skipTextHolder;
    public TextMeshProUGUI skipText;
    public string nextLevel;
    public VideoPlayer vp;
    public float seconds;
    private bool holding;
    private bool endReached = false;
    private bool finishedLoading = false;
    // Start is called before the first frame update
    void Start()
    {
        skipText = skipTextHolder.GetComponent<TextMeshProUGUI>(); //fade into scene
        //BGMFadeIn();
        StartCoroutine(CheckPlaying());
        blackScreenImage = blackScreenHolder.GetComponent<Image>();
        BlackScreenFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!vp.isPlaying && !endReached && finishedLoading) //if cutscene is finished
        {
            StartCoroutine(ExitSceneCo(nextLevel)); //load next level
            endReached = true;
        }
        if (holding) //if holding, set the time text so the player knows how long to wait
        {
            seconds -= Time.deltaTime;
            if (!skipTextHolder.activeInHierarchy)
            {
                skipTextHolder.SetActive(true);
            }
            if (seconds < 0f) //so no negatives
            {
                seconds = 0f;
            }
            skipText.text = ("Skipping in " + Mathf.RoundToInt(seconds)); //continually update the text
            if (seconds <= 0f && !endReached)
            {
                SkipCutscene();
                endReached = true;
            }
        }
        else //if released, set the text inactive and reset back to 3 seconds
        {
            if (skipTextHolder.activeInHierarchy)
            {
                skipTextHolder.SetActive(false);
            }
            if (seconds < 3f)
            {
                seconds = 3f;
            }
        }
    }

    IEnumerator CheckPlaying() //give time for the video to load up, or else isPlaying will not function as intended
    {
        yield return new WaitForSeconds(3f);
        finishedLoading = true;

    }
    public void SkipCutscene()
    {
        skipTextHolder.SetActive(false);
        StartCoroutine(ExitSceneCo(nextLevel));
    }

    public void OnHeld()
    {
        holding = true;
    }

    public void OnRelease()
    {
        holding = false;
    }
}
