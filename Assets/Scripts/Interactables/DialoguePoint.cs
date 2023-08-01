using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePoint : MonoBehaviour
{
    public AudioSource dialogueAudio;
    public AudioClip endSound;
    public bool givesItem;

    //For Dialogue
    public PlayerController pc;
    public Joystick js;
    public InteractButton ib;
    //public GameObject controls;
    public GameObject item;
    public GameObject wall;
    //public SpriteRenderer wallSprite;
    public TextMeshProUGUI textbox;
    public string[] lines;
    public string finalText;
    public Color[] textColor;
    public float textSpeed;
    public bool playOnce;
    public bool autoplay;
    public bool unlocksArea;

    private int index;
    public bool allowedToPlay = true;
    private bool inProgress = false; //Prevent Text from always showing up

    // Start is called before the first frame update
    void Start()
    {
        textbox.text = string.Empty;
        /*if (wall)
        {
            wallSprite = wall.GetComponent<SpriteRenderer>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (ib.clicksInFourthSec > 1 && inProgress)
        {
            StopAllCoroutines();
            NextLine();
        }
        if (ib.dialogueSkip && inProgress)
        {
            if (textbox.text == lines[index]) //next line when clicked if all text outputted
            {
                StopAllCoroutines();
                NextLine();
            }
            else //skip to all text outputted when clicked
            {
                StopAllCoroutines();
                textbox.text = lines[index];
                if (autoplay)
                {
                    StartCoroutine(TypeLine());
                }
            }
            ib.dialogueSkip = false;
        }
    }

    void StartDialogue()
    {
        textbox.color = textColor[index];
        textbox.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (textbox.text != lines[index])
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textbox.text += c; //print out each character individually
                yield return new WaitForSeconds(textSpeed); //wait for an extremely short while to give illusion of typing
            }
        }
        if (autoplay)
        {
            if (textbox.text == lines[index])
            {
                yield return new WaitForSeconds(1.5f);
                NextLine();
            }
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1) //If not all the segments of dialogue have been outputted
        {
            index++;
            textbox.color = textColor[index];
            textbox.text = string.Empty; //Set the textbox to empty so that the next line can play
            StartCoroutine(TypeLine());
        }
        else if (index >= lines.Length - 1 || ib.clicksInFourthSec > 1)  //once all text done do this
        {
            pc.inDialogue = false;
            textbox.text = finalText;
            inProgress = false;
            js.joystickPanel.SetActive(true);
            pc.canMove = true;
            dialogueAudio.PlayOneShot(endSound);
            if (playOnce)
            {
                allowedToPlay = false;
            }
            if (givesItem)
            {
                item.SetActive(true);
            }
            if (unlocksArea)
            {
                StartCoroutine(FadeTo(0f,0.21f));
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && allowedToPlay && !inProgress && !pc.inDialogue) //Only the player can activate the dialogue
        {
            inProgress = true;
            pc.inDialogue = true;
            StartDialogue();
            if (!autoplay)
            {
                pc.canMove = false;
                js.PointerUp();
                js.joystickPanel.SetActive(false);
            }

        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !autoplay && !pc.inDialogue)
        {
            textbox.text = string.Empty;
        }
        
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = wall.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            wall.GetComponent<Renderer>().material.color = newColor;
            yield return null;
            
        }
        wall.SetActive(false);
    }
}
