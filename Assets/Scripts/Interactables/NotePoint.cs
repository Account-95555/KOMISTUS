using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePoint : MonoBehaviour
{
    public PlayerController pc;
    public GameObject Note;
    public AudioSource bgm;
    public AudioClip sfx;
    public InteractButton ib;
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
            if (ib.clickRegistered)
            {
                Note.SetActive(true);
                bgm.PlayOneShot(sfx);
                ib.clickRegistered = false;
            }
        }
        else
        {
            Note.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            pc.noDrop = true;
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
}
