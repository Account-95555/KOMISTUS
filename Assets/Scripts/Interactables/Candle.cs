using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Candle : MonoBehaviour
{
    public CandlePuzzle cp;
    public GameObject self;
    public Image candle;
    public Sprite Green;
    public Sprite White;
    public string status;
    public string type;
    public bool colorChanged;
    //public bool ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colorChanged)
        {
            if (status == type)
            {
                cp.completeStatus += 1;
            }
            else
            {
                cp.completeStatus -= 1;
            }
            colorChanged = false;
        }
    }

    void ColourChange()
    {
        if (status == "white")
        {
            candle.sprite = Green;
            
        }
        else
        {
            candle.sprite = Green;
            
        }
        colorChanged = true;
    }
}
