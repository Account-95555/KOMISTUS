using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LogoSceneManager : SceneManagerCommon
{
    public string sceneToLoad;
    public GameObject videoHolder;
    public VideoPlayer loadingVideo;
    // Start is called before the first frame update
    void Start()
    {
        loadingVideo = videoHolder.GetComponent<VideoPlayer>();
        StartCoroutine(LoadProcess());
        //Debug.Log((float) loadingVideo.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadProcess()
    {
        yield return new WaitForSeconds(1);
        videoHolder.SetActive(true);
        yield return new WaitForSeconds((float)loadingVideo.clip.length + 1f);
        LoadScene(sceneToLoad);
    }
}
