using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RitualCircle : MonoBehaviour
{
    public CandlePuzzle cp;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cp.finished)
        {
            gameObject.GetComponent<Light2D>().color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!cp.finished)
            {
                UI.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!cp.finished)
            {
                UI.SetActive(false);
            }
            
        }
    }
}
