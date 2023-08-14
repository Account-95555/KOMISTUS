using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpezionHintArea : MonoBehaviour
{
    //Prevent camera from shifting when out of puzzle room during hint
    public bool inHintArea = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHintArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHintArea = false;
        }
    }
}
