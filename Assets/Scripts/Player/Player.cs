using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed;
    public float runSpeed;
    public bool isRunning = false;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(joystick.joystickVector.y != 0)
        {
            if (joystick.joystickDist >= 100f)
            {
                rb.velocity = new Vector2(joystick.joystickVector.x * runSpeed, joystick.joystickVector.y * runSpeed);
                isRunning = true;
            }
            else  if (joystick.joystickDist < 100f)
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
}
