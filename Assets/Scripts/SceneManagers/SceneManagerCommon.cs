using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerCommon : MonoBehaviour
{
    //public string sceneToLoad;
    public float volumeVar;
    //public float volumeRef = 0f;
    public float fadeInTime = 2f;
    public float fadeOutTime = 2f;

    public GameObject blackScreenHolder;
    public Image blackScreenImage;

    public bool toFadeIn = false;
    public bool toFadeOut = false;
    public AudioSource BGMSource; //Background Music Source
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public virtual void BGMFadeIn()
    {
        toFadeIn = true;
    }
    public virtual void BGMFadeOut()
    {
        toFadeOut = true;
    }
    public virtual void BlackScreenFadeIn()
    {
        StartCoroutine(BlackScreenFadeInCo());
    }

    IEnumerator BlackScreenFadeInCo()
    {
        blackScreenImage.CrossFadeAlpha(1f, 0f, true);
        blackScreenImage.CrossFadeAlpha(0f, fadeInTime, true);
        yield return new WaitForSecondsRealtime(fadeInTime);
        blackScreenHolder.SetActive(false);
    }

    public virtual void BlackScreenFadeOut()
    {
        blackScreenHolder.SetActive(true);
        blackScreenImage.CrossFadeAlpha(0f, 0f, true);
        blackScreenImage.CrossFadeAlpha(1f, fadeOutTime, true);
    }
    public virtual void ExitScene(string sceneToLoad)
    {
        StartCoroutine(ExitSceneCo(sceneToLoad));
    }

    public virtual IEnumerator ExitSceneCo(string sceneToLoad)
    {
        BGMFadeOut();
        BlackScreenFadeOut();
        yield return new WaitForSecondsRealtime(fadeOutTime);
        LoadScene(sceneToLoad);
    }

}
