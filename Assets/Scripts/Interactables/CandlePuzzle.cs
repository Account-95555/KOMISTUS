using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandlePuzzle : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip complete;
    public GameObject item;
    public int completeStatus;
    public bool finished;
    private Light2D l2d;

    // Start is called before the first frame update
    void Start()
    {
        l2d = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (completeStatus == 5 && !finished)
        {
            StartCoroutine(Finished());
            finished = true;
        }
    }

    IEnumerator Finished()
    {
        l2d.color = Color.black; //remove the light to show puzzle is complete
        bgm.PlayOneShot(complete);
        yield return new WaitForSeconds(2f);
        item.SetActive(true);
        //gameObject.SetActive(false);
    }

    
}