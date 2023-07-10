using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemRequired : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public GameObject attachedObject;
    public string requiredObject;
    public bool givesItem;
    public InventoryV2 iv2;
    public InventoryButtonV2 ib2;
    public InteractButton ib;

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
                textbox.text = "You require a " + requiredObject + " for this...";
                
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
            if (ib.clickRegistered)
            {
                textbox.text = "You require a " + requiredObject + " for this...";
                ib.clickRegistered = false;
            }
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ib.clickRegistered)
            {
                textbox.text = "You require a " + requiredObject + " for this...";
                ib.clickRegistered = false;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            ib2.atItemRequired = false;
            textbox.text = string.Empty;
        }
        
    }
}
