using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPoint : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public string textToShow;
    public bool deleteAfterExit;
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
        if (other.CompareTag("Player"))
        {
            textbox.text = textToShow;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textbox.text = "";
            if (deleteAfterExit)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
