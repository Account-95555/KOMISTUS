using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Door : MonoBehaviour
{
    //public GameObject shadowCaster;
    public GameObject player;
    public GameObject enemy;
    public Transform finalPos;
    public GameObject blackScreenHolder;
    public Image blackScreen;
    //public GameObject removeCollider;
    //public GameObject player;
    public bool isTrigger = false;
    public bool isVertical;
    public bool teleports;

    private Color doorColor;
    private bool isEntered = false;
    private bool telCo;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        if (blackScreenHolder != null)
        {
            blackScreen = blackScreenHolder.GetComponent<Image>();
        }
        
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
            if (teleports && !telCo)
            {
                StartCoroutine(TeleportCo());
            }
            else
            {
                doorColor.a = Mathf.MoveTowards(doorColor.a, 0f, 1f * Time.deltaTime);
            }
            
        }
        else
        {
            doorColor.a = Mathf.MoveTowards(doorColor.a, 1f, 1f * Time.deltaTime);
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            isEntered = true;
            if (isTrigger)
            {
                enemy.SetActive(true);
            }
            /*if (isVertical && other.GetComponent<SpriteRenderer>() != null)
            {
                other.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }*/
            //shadowCaster.SetActive(false);
            //GetComponent<ShadowCaster2D>().enabled = false;
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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            isEntered = false;
            //shadowCaster.SetActive(true);
            //GetComponent<ShadowCaster2D>().enabled = true;
            if (isVertical && other.GetComponent<SpriteRenderer>() != null)
            {
                other.GetComponent<SpriteRenderer>().sortingOrder = 6;
            }
        }
    }

    IEnumerator TeleportCo()
    {
        telCo = true;
        blackScreenHolder.SetActive(true);
        blackScreen.CrossFadeAlpha(0f, 0f, false);
        blackScreen.CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = finalPos.position;
        blackScreen.CrossFadeAlpha(0f, 1.25f, false);
        yield return new WaitForSeconds(1.25f);
        blackScreenHolder.SetActive(false);
        telCo = false;
    }
}
