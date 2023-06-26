using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject player;
    public GameObject attachedItem;
    public GameObject inventoryButton;
    public AudioSource dropSource;
    public AudioClip dropSound;
    public WearyMeter wm;
    public Safe sf;
    public bool isHolding = false;
    public bool isFalling = false;
    public string itemName;
    public float yFall;
    public float newYVal;

    private InventoryButton ib;
    // Start is called before the first frame update
    void Start()
    {
        ib = inventoryButton.GetComponent<InventoryButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ib.dropItem == true)
        {
            if (sf.inRange)
            {
                ib.dropItem = false;
                inventoryButton.SetActive(false);
                sf.keyInserted = true;
                sf.DropSecretItem();

            }
            else
            {
                ib.dropItem = false;
                inventoryButton.SetActive(false);
                attachedItem.SetActive(true);
                yFall = player.transform.position.y;
                newYVal = player.transform.position.y - 0.5f;
                attachedItem.transform.position = new Vector2(player.transform.position.x + 0.5f, player.transform.position.y);
                isFalling = true;
            }
        }
        if (isFalling)
        {
            if (attachedItem.transform.position.y > newYVal)
            {
                attachedItem.transform.position = new Vector2(attachedItem.transform.position.x, yFall);
                yFall -= 0.01f;
            }
            else
            {
                wm.wearyVal += 10f;
                dropSource.PlayOneShot(dropSound);
                isFalling = false;
            }
            
        }
    }
}
