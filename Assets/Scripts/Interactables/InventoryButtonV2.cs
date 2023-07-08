using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonV2 : MonoBehaviour
{
    public InventoryV2 iv2;
    public GameObject player;
    public bool atItemRequired;
    public bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed()
    {
        if (iv2.isHolding)
        {
            if (atItemRequired)
            {
                pressed = true;
            }
            else
            {
                iv2.storedItem = "none";
                iv2.attachedObject.transform.position = player.transform.position;
                iv2.attachedObject.SetActive(true);
                iv2.isHolding = false;
            }

        }
        
    }
}
