using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject player;
    public GameObject attachedItem;
    public GameObject inventoryButton;
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
            Instantiate(attachedItem, player.transform.position, Quaternion.identity);
            ib.dropItem = false;
        }
    }
}
