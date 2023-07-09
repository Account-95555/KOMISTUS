using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazard : MonoBehaviour
{
    public AudioSource hazardAudio;
    public AudioClip hazardSFX;
    public GameObject wearyObject;
    private WearyMeter wm;
    //private WearyMeter wm;
    // Start is called before the first frame update
    void Start()
    {
        wm = wearyObject.GetComponent<WearyMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wm.wearyVal += 16.9f;
            hazardAudio.PlayOneShot(hazardSFX);
        }
    }
}
