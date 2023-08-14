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
        skipText = skipTextHolder.GetComponent<TextMeshProUGUI>();
        //BGMFadeIn();
        StartCoroutine(CheckPlaying());
        blackScreenImage = blackScreenHolder.GetComponent<Image>();
        BlackScreenFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!vp.isPlaying && !endReached && finishedLoading)
        {
            StartCoroutine(ExitSceneCo(nextLevel));
            endReached = true;
        }
        if (holding)
        {
            seconds -= Time.deltaTime;
            if (!skipTextHolder.activeInHierarchy)
            {
                skipTextHolder.SetActive(true);
            }
            if (seconds < 0f)
            {
                seconds = 0f;
            }
            skipText.text = ("Skipping in " + Mathf.RoundToInt(seconds));
            if (seconds <= 0f && !endReached)
            {
                SkipCutscene();
                endReached = true;
            }
        }
        else
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

    IEnumerator CheckPlaying()
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
