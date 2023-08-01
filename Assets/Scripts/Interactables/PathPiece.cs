using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPiece : MonoBehaviour
{

    public PathPuzzle pp;

    public Sprite correct;
    public Sprite wrong;
    public Sprite normal;

    public bool isCorrect;
    public bool reset;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pp.reset && sr.sprite != normal)
        {
            sr.sprite = normal;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Feet"))
        {
            if (isCorrect) //change the sprites accordingly
            {
                sr.sprite = correct;
            }
            else
            {
                sr.sprite = wrong;
                pp.wrongStep = true; //reset the player back to the start and reset the status of the puzzle
            }
        }
    }
}
