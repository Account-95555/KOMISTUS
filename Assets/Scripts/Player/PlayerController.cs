using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Joystick joystick;
    public GameObject footstepObject;
    public AudioSource footstepAudioSource;
    public float moveSpeed;
    public float runSpeed;
    public bool isRunning = false;
    public bool isHiding = false;
    public bool isDead = false;
    public bool canMove = true;
    public string causeOfDeath;
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("CauseOfDeath", "none");
        causeOfDeath = PlayerPrefs.GetString("CauseOfDeath");
        footstepAudioSource = footstepObject.GetComponent<AudioSource>();
        footstepAudioSource.volume = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("isDragging", joystick.isDragging);
        player.localScale = new Vector3(Mathf.Sign(joystick.joystickVector.x), 1f, 1f); //change where player is facing
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
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (joystick.joystickVector.y != 0)
            {
                if (joystick.joystickDist >= 100f) //if joystick drag is 100 units or greater from touch point, sprint
                {
                    rb.velocity = new Vector2(joystick.joystickVector.x * runSpeed, joystick.joystickVector.y * runSpeed);
                    isRunning = true;
                }
                else if (joystick.joystickDist < 100f) //else walk
                {
                    rb.velocity = new Vector2(joystick.joystickVector.x * moveSpeed, joystick.joystickVector.y * moveSpeed);
                    isRunning = false;
                }

            }
            else //if not being dragged stop entirely
            {
                rb.velocity = Vector2.zero;
                isRunning = false;
            }
        }
        else //stop entirely also if the player is unable to move
        {
            rb.velocity = Vector2.zero;
            isRunning = false;
        }
    }
}
