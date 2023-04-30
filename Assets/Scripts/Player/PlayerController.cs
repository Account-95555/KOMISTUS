using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Joystick joystick;
    public float moveSpeed;
    public float runSpeed;
    public bool isRunning = false;
    public bool canMove = true;
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("isDragging", joystick.isDragging);
        player.localScale = new Vector3(Mathf.Sign(joystick.joystickVector.x), 1f, 1f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (joystick.joystickVector.y != 0)
            {
                if (joystick.joystickDist >= 100f)
                {
                    rb.velocity = new Vector2(joystick.joystickVector.x * runSpeed, joystick.joystickVector.y * runSpeed);
                    isRunning = true;
                }
                else if (joystick.joystickDist < 100f)
                {
                    rb.velocity = new Vector2(joystick.joystickVector.x * moveSpeed, joystick.joystickVector.y * moveSpeed);
                    isRunning = false;
                }

            }
            else
            {
                rb.velocity = Vector2.zero;
                isRunning = false;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            isRunning = false;
        }
    }
}
