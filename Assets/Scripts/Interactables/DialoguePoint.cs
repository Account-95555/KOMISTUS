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
    public GameObject controls;
    public TextMeshProUGUI textbox;
    public string[] lines;
    public string finalText;
    public float textSpeed;

    private int index;
    private bool inProgress = false; //Prevent Text from always showing up

    // Start is called before the first frame update
    void Start()
    {
        textbox.text = string.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inProgress)
        {
            if (textbox.text == lines[index]) //next line when clicked if all text outputted
            {
                NextLine();
            }
            else //skip to all text outputted when clicked
            {
                StopAllCoroutines();
                textbox.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        inProgress = true;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textbox.text += c; //print out each character individually
            yield return new WaitForSeconds(textSpeed); //wait for an extremely short while to give illusion of typing
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1) //If not all the segments of dialogue have been outputted
        {
            index++;
            textbox.text = string.Empty; //Set the textbox to empty so that the next line can play
            StartCoroutine(TypeLine());
        }
        else //once all text done do this
        {
            textbox.text = finalText;
            inProgress = false;
            controls.SetActive(true);
            pc.canMove = true;
            dialogueAudio.PlayOneShot(endSound);
            if (givesItem)
            {

            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //Only the player can activate the dialogue
        {
            StartDialogue();
            pc.canMove = false;
            js.isDragging = false;
            controls.SetActive(false);
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textbox.text = string.Empty;
        }
        
    }
}
