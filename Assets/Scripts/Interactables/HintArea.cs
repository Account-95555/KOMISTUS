using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintArea : MonoBehaviour
{
    public KeycodeObject safe; //only for lvl 1
    public WearyMeter wm;
    public LevelEnd le;
    public GameObject hintTextHolder;
    public TextMeshProUGUI hintText;
    public Animator hintAnim;
    public string hintToDisplay;
    public int hintTimer = 0;
    public int timeToWait = 0;
    public bool hintDrop;
    private bool inHintArea = false;
    public bool hintStay = false;
    private bool coRunning = false;
    private bool coRunning2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hintTextHolder.activeInHierarchy)
        {
            hintAnim.SetBool("HintGoUp", hintDrop);
            hintAnim.SetBool("HintStay", hintStay);
        }
        
        if (inHintArea && !coRunning) //count how long the player is in the hint area
        {
            StartCoroutine(HintSecondsCounter());
        }

        if (inHintArea && hintTimer >= timeToWait && !hintTextHolder.activeInHierarchy && !wm.canBeChased && !le.originDestroyed) //if hint timer is equal to the waiting timer, show the hint
        {
            hintStay = true;
            if (!hintTextHolder.activeInHierarchy)
            {
                
                hintTextHolder.SetActive(true);
            }
            
        }

        if (safe)
        {
            if (safe.correctCode)
            {
                gameObject.SetActive(false);
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            hintDrop = false;
            hintText.text = hintToDisplay;
            inHintArea = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHintArea = false;
            hintStay = false;
            //hintTimer = 0;
            if (!coRunning2)
            {
                StartCoroutine(HintAreaExit());
            }
        }
    }

    IEnumerator HintSecondsCounter()
    {
        coRunning = true;
        yield return new WaitForSeconds(1f);
        hintTimer += 1;
        coRunning = false;
    }

    IEnumerator HintAreaExit()
    {
        coRunning2 = true;
        hintDrop = true;
        yield return new WaitForSeconds(1f);
        hintTextHolder.SetActive(false);
        coRunning2 = false;
    }
}
