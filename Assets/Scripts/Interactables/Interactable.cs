using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public GameObject interactButtonObj;
    public GameObject inventoryButton;
    public GameObject attachedItem;
    public GameObject inventory;
    public string textPointText;
    public string itemName;
    public bool isLockedCloset;
    //public bool isItemStore;
    public bool isItem;
    public bool isTextPoint;
    public Sprite itemSprite;
    public Image itemImage;
    private Inventory inv;
    private InteractButton ib;
    private bool inRange;
    //private bool checkForText = false;
    // Start is called before the first frame update
    void Start()
    {
        inventoryButton.SetActive(false);
        inv = inventory.GetComponent<Inventory>();
        ib = interactButtonObj.GetComponent<InteractButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (ib.clickRegistered)
            {
                ib.clickRegistered = false;
                StartCoroutine(onClick());
            }
        }
    }

    IEnumerator onClick()
    {
        // coroutine is here to make the text blank after 5 seconds
        if (isLockedCloset)
        {
            textbox.text = ("The closet is locked...");
        }
        //if (isItemStore)
        //{
        if (isItem)
        {
            inv.isHolding = true;
            inventoryButton.SetActive(true);
            textbox.text = (itemName + " has been picked up.");
            itemImage.sprite = itemSprite;
            inv.attachedItem = attachedItem;
            gameObject.SetActive(false);
        }
        //else
        //{
            //textbox.text = ("Nothing in here...");
        //}
        //}
        yield return new WaitForSeconds(3f);
        textbox.text = ("");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the player is colliding with the object
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            if (isTextPoint)
            {
                textbox.text = textPointText;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            if (isTextPoint)
            {
                textbox.text = ("");
            }
        }
    }
}
