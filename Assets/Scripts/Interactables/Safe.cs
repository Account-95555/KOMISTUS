using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Safe : MonoBehaviour
{
    public bool inRange;
    public bool keyInserted;
    public GameObject mask;
    public TextMeshProUGUI textbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keyInserted)
        {
            inRange = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Player") && !keyInserted)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    public void DropSecretItem()
    {
        Instantiate(mask, transform.position, Quaternion.identity);
    }
}
