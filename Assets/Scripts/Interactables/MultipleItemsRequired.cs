using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleItemsRequired : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip correct;
    public GameObject door;
    public int itemsRequired;
    public int status = 0;
    private bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (status == itemsRequired)
        {
            door.SetActive(false);
            if (!complete) //prevent win sound from continually activating
            {
                bgm.PlayOneShot(correct);
                complete = true;
            }
        }
    }
}
