using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class InteractButton : MonoBehaviour
{
    public Animator anim;
    //public Button interactButton;
    public int clicksInFourthSec = 0;
    public bool clickRegistered;
    public bool dialogueSkip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isInteracting", clickRegistered);
    }

    public void OnClick()
    {
        clickRegistered = true;
        StartCoroutine(ClickCooldown());
    }

    public void DialogueClick()
    {
        clicksInFourthSec += 1;
        dialogueSkip = true;
        StartCoroutine(SkipAll());
        StartCoroutine(ClickCooldown());
    }

    IEnumerator ClickCooldown() //click debounce
    {
        yield return new WaitForSeconds(0.01f);
        clickRegistered = false;
        dialogueSkip = false;
    }

    IEnumerator SkipAll()
    {
        yield return new WaitForSeconds(0.25f);
        clicksInFourthSec = 0;
    }

}
