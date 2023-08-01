using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePoint : MonoBehaviour
{
    public GameObject Note;
    public AudioSource bgm;
    public AudioClip sfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Note.SetActive(true);
            bgm.PlayOneShot(sfx);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Note.SetActive(false);
        }
    }
}
