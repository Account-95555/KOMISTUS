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
    public bool isHolding = false;

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
            inventoryButton.SetActive(false);
            attachedItem.transform.position = new Vector2(player.transform.position.x +0.5f, player.transform.position.y - 1f);
            attachedItem.SetActive(true);
            wm.wearyVal += 10f;
            dropSource.PlayOneShot(dropSound);
            ib.dropItem = false;
        }
    }
}
