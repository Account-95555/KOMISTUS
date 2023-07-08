using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRequired : MonoBehaviour
{
    public GameObject attachedObject;
    public string requiredObject;
    public bool givesItem;
    public InventoryV2 iv2;
    public InventoryButtonV2 ib2;

    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && ib2.pressed)
        {
            if (iv2.storedItem == requiredObject)
            {
                iv2.storedItem = "none";
                iv2.isHolding = false;
                if (givesItem)
                {
                    attachedObject.SetActive(true);
                }
                
            }
            else
            {
                Debug.Log("NO");
                
            }
            ib2.pressed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            inRange = true;
            ib2.atItemRequired = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            ib2.atItemRequired = false;
        }
        
    }
}
