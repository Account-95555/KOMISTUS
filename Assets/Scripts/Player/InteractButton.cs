using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class InteractButton : MonoBehaviour
{
    //public Button interactButton;

    public bool clickRegistered;
    public bool dialogueSkip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        clickRegistered = true;
        dialogueSkip = true;
        StartCoroutine(ClickCooldown());
    }

    IEnumerator ClickCooldown() //click debounce
    {
        yield return new WaitForSeconds(0.01f);
        clickRegistered = false;
        dialogueSkip = false;
    }
}
