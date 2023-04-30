using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject shadowCaster;

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
            shadowCaster.SetActive(false);
        }
        else
        {
            doorColor.a = Mathf.MoveTowards(doorColor.a, 1f, 1f * Time.deltaTime);
            shadowCaster.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEntered = true;
        }
   
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEntered = false;
        }
    }
}
