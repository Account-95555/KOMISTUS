using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeycodeObject : MonoBehaviour
{
    public InteractButton ib;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip press;
    public AudioSource bgm;
    public GameObject door;
    public GameObject keypad;
    public GameObject attachedObject;
    public bool inRange;
    public bool correctCode;
    public bool isDoor;
    public bool givesItem;
    public string codeNumber;
    public string playerInput;
    public string hint;
    public TextMeshProUGUI currentInput;
    public TextMeshProUGUI textbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !correctCode)
        {
            keypad.SetActive(true);
            textbox.text = hint;
            currentInput.text = playerInput;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            keypad.SetActive(false);
            textbox.text = "";
        }
    }
    public void NumberPress(int number)
    {
        if (playerInput.Length < codeNumber.Length && !correctCode)
        {
            bgm.PlayOneShot(press);
            playerInput = playerInput + number;
        }
    }

    public void SpecialPress(string key)
    {
        if (!correctCode)
        {
            if (key == "Delete")
            {
                bgm.PlayOneShot(press);
                playerInput = string.Empty;
            }
            else if (key == "Enter")
            {
                if (playerInput == codeNumber)
                {
                    StartCoroutine(CorrectCode());
                    correctCode = true;
                }
                else
                {
                    bgm.PlayOneShot(wrong);
                    playerInput = "INCORRECT - DEL";
                }
            }
        }
        
    }
    
    IEnumerator CorrectCode()
    {
        textbox.text = "";
        bgm.PlayOneShot(correct);
        playerInput = "ACCESS GRANTED";
        yield return new WaitForSeconds(1.5f);
        if (isDoor)
        {
            door.SetActive(false);
        }
        if (givesItem)
        {
            attachedObject.SetActive(true);
        }
        keypad.SetActive(false);
    }

}
