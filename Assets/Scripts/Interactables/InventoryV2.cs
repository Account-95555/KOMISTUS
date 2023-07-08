using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryV2 : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject attachedObject;
    public Image itemImage;
    public string storedItem;
    public bool isHolding;

    // Start is called before the first frame update
    void Start()
    {
        storedItem = "none";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHolding)
        {
            inventoryButton.SetActive(false);
        }
        else
        {
            if(inventoryButton.activeInHierarchy == false)
            {
                inventoryButton.SetActive(true);
            }

        }
    }
}
