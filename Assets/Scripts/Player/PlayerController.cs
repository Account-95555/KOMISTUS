using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Joystick joystick;
    public GameObject footstepObject;
    public GameObject wearyObject;
    public GameObject inventoryButton;
    public AudioSource footstepAudioSource;
    public float moveSpeed;
    public float runSpeed;
    public float lastXScale = 1f;

    public bool isRunning = false;
    public bool isHiding = false;
    public bool isDead = false;
    public bool canMove = true;
    public bool noDrop = false; //prevent player from dropping near closets
    public bool inInteractibleRange = false; //prevent player from dropping near interactibles
    public bool inDialogue = false;
    //public bool isHolding = false;
    
    public string causeOfDeath;
    
    private Rigidbody2D rb;
    private Animator anim;
    private WearyMeter wm;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("CauseOfDeath", "none");
        causeOfDeath = PlayerPrefs.GetString("CauseOfDeath");
        footstepAudioSource = footstepObject.GetComponent<AudioSource>();
        footstepAudioSource.volume = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wm = wearyObject.GetComponent<WearyMeter>();
    }
    void Update()
    {
        anim.SetFloat("joystickDist",joystick.joystickDist);
        player.localScale = new Vector3(lastXScale, player.localScale.y, player.localScale.z); //change where player is facing
        if (canMove)
        {
            if (joystick.joystickVector.y != 0)
            {
                if (isRunning)
                {
                    footstepAudioSource.pitch = 1.2f;
                    footstepAudioSource.volume = 0.25f;
                }
                else
                {
                    footstepAudioSource.pitch = 1f;
                    footstepAudioSource.volume = 0.15f;
                    //wm.wearyVal += 15f * Time.deltaTime;
                }
            }
            else
            {
                footstepAudioSource.volume = 0f;
            }
        }
        else
        {
            footstepAudioSource.volume = 0f;
        }

        /*if (isHolding)
        {
            inventoryButton.SetActive(true);
        }
        else
        {
            inventoryButton.SetActive(false);
        }*/
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (joystick.joystickVector.y != 0)
            {
                if (joystick.joystickDist >= 110f) //if joystick drag is 100 units or greater from touch point, sprint
                {
                    rb.velocity = new Vector2(joystick.joystickVector.x * runSpeed, joystick.joystickVector.y * runSpeed);
                    isRunning = true;
                    lastXScale = Mathf.Sign(joystick.joystickVector.x);
                }
                else if (joystick.joystickDist < 110f) //else walk
                {
                    rb.velocity = new Vector2(joystick.joystickVector.x * moveSpeed, joystick.joystickVector.y * moveSpeed);
                    isRunning = false;
                    lastXScale = Mathf.Sign(joystick.joystickVector.x);
                }

            }
            else //if not being dragged stop entirely
            {
                rb.velocity = Vector2.zero;
                isRunning = false;
                joystick.joystickDist = 0;
            }
        }
        else //stop entirely also if the player is unable to move
        {
            joystick.joystickDist = 0;
            rb.velocity = Vector2.zero;
            isRunning = false;
        }
    }

}
