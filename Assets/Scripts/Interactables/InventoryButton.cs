using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    public PlayerController pc;
    public TextMeshProUGUI dialogue;
    public bool dropItem = false;
    public bool coroutineInProg = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        if (!pc.inClosetRange)
        {
            dropItem = true;
        }
        else
        {
            if (!coroutineInProg)
            {
                StartCoroutine(TooCloseToCloset());
            }
        }
    }

    IEnumerator TooCloseToCloset()
    {
        coroutineInProg = true;
        dialogue.text = "Too close to hiding spot, cannot drop...";
        dialogue.CrossFadeAlpha(0f, 0f, false);
        dialogue.CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(2f);
        dialogue.CrossFadeAlpha(0f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        dialogue.text = "";
        coroutineInProg = false;
    }
}
