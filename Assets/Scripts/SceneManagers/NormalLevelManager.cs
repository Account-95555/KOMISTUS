using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalLevelManager : SceneManagerCommon
{
    public GameObject player;
    public GameObject mobileControls;
    public GameObject footstepObject;
    public GameObject deathImageHolder;
    
    public Image deathImage;
    
    public AudioClip jumpscareSFX;
    public AudioClip jumpSupport;
    public PlayerController pc;

    private bool deathCoPlaying = false;

    
    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        blackScreenImage = blackScreenHolder.GetComponent<Image>();
        deathImage = deathImageHolder.GetComponent<Image>();
        BGMFadeIn();
        BlackScreenFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isDead && deathCoPlaying != true)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        pc.canMove = false;
        deathCoPlaying = true;
        footstepObject.SetActive(false);
        mobileControls.SetActive(false);
        deathImageHolder.SetActive(true);
        blackScreenHolder.SetActive(true);
        BGMSource.PlayOneShot(jumpscareSFX);
        BGMSource.PlayOneShot(jumpSupport);
        BGMSource.clip = null;
        deathImage.CrossFadeAlpha(0f, 0f, false);
        deathImage.CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        //deathImage.CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(2.56f);
        //deathImage.CrossFadeAlpha(1f, 0.5f, false);
        LoadScene("DeathScene");
    }
}
