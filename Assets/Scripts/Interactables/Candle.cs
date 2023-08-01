using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    public InteractButton ib;
    public CandlePuzzle cp;
    //public GameObject self;
    public Sprite Green;
    public Sprite White;
    private string status;
    public string type;
    public bool correct;
    private bool colorChanged;
    private bool inRange;
    private SpriteRenderer sr;
    private Light2D l2d;
    //public bool ;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        l2d = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ib.clickRegistered && inRange && !cp.finished)
        {
            ColourChange();
            ib.clickRegistered = false;
        }
        if (colorChanged) //if the color is changed
        {
            if (status == type) //if the current color is equal to the required color
            {
                cp.completeStatus += 1;
                correct = true; //1 step closer to completion
            }
            else
            {
                if (correct)
                {
                    cp.completeStatus -= 1;
                }
                correct = false;
                
            }
            colorChanged = false; //prevent loop from running infinitely
            
        }
    }

    public void ColourChange()
    {
        if (l2d.enabled == false)
        {
            l2d.enabled = true;
        }
        if (status == "white") //if color is white
        {
            l2d.color = Color.green;
            sr.sprite = Green; //switch to green
            status = "green";

        }
        else //vice versa
        {
            l2d.color = Color.white;
            sr.sprite = White;
            status = "white";

        }
        colorChanged = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}