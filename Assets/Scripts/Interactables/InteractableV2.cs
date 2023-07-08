using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableV2 : MonoBehaviour
{
    public GameObject spezionParticles;

    public bool isItem;
    public string itemName;
    private bool inRange;
    private bool isAdded = false;
    //Classes
    public Spezion sp;
    public InteractButton ib;
    public InventoryV2 iv2;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.isActive)
        {
            spezionParticles.SetActive(true);
        }
        else
        {
            spezionParticles.SetActive(false);
        }
        //onClick
        if (ib.clickRegistered && inRange)
        {
            if (isItem)
            {
                iv2.isHolding = true;
                iv2.attachedObject = gameObject;
                iv2.itemImage.sprite = sr.sprite;
                iv2.storedItem = itemName;
                gameObject.SetActive(false);
            }
            ib.clickRegistered = false;
        }
        if (sr.isVisible && isAdded == false)
        {
            sp.interactiblesOnScreen += 1;
            isAdded = true;
        }
        else if (!sr.isVisible && isAdded == true)
        {
            sp.interactiblesOnScreen -= 1;
            isAdded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
