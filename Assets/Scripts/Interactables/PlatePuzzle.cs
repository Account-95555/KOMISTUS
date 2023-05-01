using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePuzzle : MonoBehaviour
{
    //public GameObject plate;

    public int currentNum = 1;
    public GameObject door;
    private SpriteRenderer sr;
    public bool reset = false;
    public bool allCorrect = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNum == 5)
        {
            allCorrect = true;
            door.SetActive(false);
        }
    }

}
