using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonV2 : MonoBehaviour
{
    public InventoryV2 iv2;
    public WearyMeter wm;
    public PlayerController pc;
    public AudioSource bgm;
    public AudioClip drop;
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
        if (iv2.isHolding) //item drop conditions
        {
            if (atItemRequired) //if the player drops at an unlockable, unlock
            {
                pressed = true;
            }
            else
            {
                if (!pc.noDrop) //if allowed to drop, drop
                {
                    iv2.storedItem = "none";
                    iv2.attachedObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.69f, player.transform.position.z);
                    iv2.attachedObject.SetActive(true);
                    bgm.PlayOneShot(drop);
                    wm.wearyVal += 11f;
                    iv2.isHolding = false;
                }
                
            }

        }
        
    }
}
