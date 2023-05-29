using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : MonoBehaviour
{
    //public GameObject shadowCaster;
    public GameObject enemy;

    public bool isTrigger = false;

    private Color doorColor;
    private bool isEntered = false;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        doorColor = Color.white;
        doorColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = doorColor;
        if (isEntered)
        {
            doorColor.a = Mathf.MoveTowards(doorColor.a, 0f, 1f * Time.deltaTime);
        }
        else
        {
            doorColor.a = Mathf.MoveTowards(doorColor.a, 1f, 1f * Time.deltaTime);
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEntered = true;
            if (isTrigger)
            {
                enemy.SetActive(true);
            }
            //shadowCaster.SetActive(false);
            GetComponent<ShadowCaster2D>().enabled = false;
        }
   
    }

    /*void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shadowCaster.SetActive(false);
        }
    }*/
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEntered = false;
            //shadowCaster.SetActive(true);
            GetComponent<ShadowCaster2D>().enabled = true;
        }
    }
}
