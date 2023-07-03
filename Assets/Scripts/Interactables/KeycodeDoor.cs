using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeycodeDoor : MonoBehaviour
{
    public InteractButton ib;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip press;
    public AudioSource self;
    public GameObject door;
    public GameObject keypad;
    public bool inRange;
    public string codeNumber;
    public string playerInput;
    public TextMeshProUGUI currentInput;
    public TextMeshProUGUI textbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            keypad.SetActive(true);
            currentInput.text = playerInput;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            textbox.text = "Wonder why the arrangement of the books on the shelves look weird...";
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
        if (playerInput.Length < 4)
        {
            self.PlayOneShot(press);
            playerInput = playerInput + number;
        }
    }

    public void SpecialPress(string key)
    {
        if (key == "Delete")
        {
            self.PlayOneShot(press);
            playerInput = string.Empty;
        }
        else if (key == "Enter")
        {
            if (playerInput == codeNumber)
            {
                StartCoroutine(CorrectCode());
            }
            else
            {
                self.PlayOneShot(wrong);
                playerInput = "INCORRECT - DEL TO RESET";
            }
        }
    }
    
    IEnumerator CorrectCode()
    {
        textbox.text = "";
        self.PlayOneShot(correct);
        playerInput = "ACCESS GRANTED";
        yield return new WaitForSeconds(1.5f);
        door.SetActive(false);
        keypad.SetActive(false);
    }

}
