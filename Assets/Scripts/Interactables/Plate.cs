using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public int sequenceNum;
    public GameObject platePuzzleObj;
    public Sprite corrSprite;
    public Sprite wrongSprite;
    public Sprite neutralSprite;
    private bool correct = false;
    //public bool reset = false;
    private SpriteRenderer sr;
    private PlatePuzzle pp;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pp = platePuzzleObj.GetComponent<PlatePuzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pp.reset == true)
        {
             sr.sprite = wrongSprite;
             correct = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (pp.allCorrect == false)
        {
            if (correct == false)
            {
                if (sequenceNum == pp.currentNum)
                {
                    pp.reset = false;
                    sr.sprite = corrSprite;
                    pp.currentNum += 1;
                    correct = true;
                }
                else
                {
                    pp.currentNum = 1;
                    pp.reset = true;
                }
            }
        }
    }
}
