using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandlePuzzle : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip complete;
    public GameObject item;
    public int completeStatus;
    public bool finished;

    // Start is called before the first frame update
    void Start()
    {

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
        bgm.PlayOneShot(complete);
        yield return new WaitForSeconds(2f);
        item.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(true);
        }

    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

    }
}