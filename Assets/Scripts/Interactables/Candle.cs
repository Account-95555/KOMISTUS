using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Candle : MonoBehaviour
{
    public CandlePuzzle cp;
    //public GameObject self;
    public Image candle;
    public Sprite Green;
    public Sprite White;
    private string status;
    public string type;
    public bool correct;
    private bool colorChanged;
    //public bool ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (status == "white") //if color is white
        {
            candle.sprite = Green; //switch to green
            status = "green";

        }
        else //vice versa
        {
            candle.sprite = White;
            status = "white";

        }
        colorChanged = true;
    }
}