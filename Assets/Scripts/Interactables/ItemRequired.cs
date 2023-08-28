using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemRequired : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public AudioSource bgm;
    public AudioClip correct;
    public GameObject attachedObject;
    public GameObject door;
    public GameObject secondDoor;
    public string requiredObject;
    public bool activatesDoor;
    public bool anotherDoor;
    public bool givesItem;
    public bool multipleItems;
    public PlayerController pc;
    public InventoryV2 iv2;
    public InventoryButtonV2 ib2;
    public MultipleItemsRequired mir;
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
            if (iv2.storedItem == requiredObject) //if item in hand is item required, do stuff
            {
                iv2.storedItem = "none";
                iv2.isHolding = false;
                if (givesItem)
                {
                    attachedObject.SetActive(true);
                }
                if (activatesDoor)
                {
                    if (anotherDoor)
                    {
                        secondDoor.SetActive(true);
                    }
                    door.SetActive(false);
                }
                if (multipleItems)
                {
                    mir.status += 1;
                }
                bgm.PlayOneShot(correct);
                
            }
            else
            {
                if (textbox.text == "" && !pc.inDialogue)
                {
                    textbox.text = "You require a " + requiredObject + " for this..."; //prevent any dialogue overwrites
                }
                
                
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
                if (textbox.text == "" && !pc.inDialogue)
                {
                    textbox.text = "You require a " + requiredObject + " for this..."; //prevent any dialogue overwrites
                }
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
                if (textbox.text == "" && !pc.inDialogue)
                {
                    textbox.text = "You require a " + requiredObject + " for this..."; //prevent any dialogue overwrites
                }
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
            if (!pc.inDialogue)
            {
                textbox.text = string.Empty;
            }
            
        }
        
    }

    //ItemRequired - just for me to copy and paste for debugging :D
}
