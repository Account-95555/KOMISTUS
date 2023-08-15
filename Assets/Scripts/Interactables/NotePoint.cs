using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePoint : MonoBehaviour
{
    public PlayerController pc;
    public GameObject note;
    public GameObject jumpscareHolder;
    public Image jumpscareImage;
    public AudioSource bgm;
    public AudioClip sfx;
    public AudioClip jumpSound;
    public InteractButton ib;
    public bool isCursed;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (ib.clickRegistered) //when clicked, show note
            {
                if (note)
                {
                    note.SetActive(true);
                }
                bgm.PlayOneShot(sfx); //feedback
                if (isCursed)
                {
                    StartCoroutine(Jumpscare());
                }
                ib.clickRegistered = false;
            }
        }
        else //if player walks away set note inactive
        {
            if (note)
            {
                note.SetActive(false);
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            pc.noDrop = true; //prevent player from dropping items at note to stop collider pickup issues
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            pc.noDrop = false;
        }
    }

    IEnumerator Jumpscare()
    {
        jumpscareHolder.SetActive(true);
        bgm.PlayOneShot(jumpSound);
        jumpscareImage.CrossFadeAlpha(1f, 0f, false);
        yield return new WaitForSeconds(0.5f);
        jumpscareImage.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
        jumpscareHolder.SetActive(false);
    }
}
