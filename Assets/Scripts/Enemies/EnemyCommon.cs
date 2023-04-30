using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    public GameObject player;
    public PlayerController pc;
    public float moveSpeed;
    public GameObject jumpscare;
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void MoveUp()
    {
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
    }
    public virtual void MoveDown()
    {
        rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
    }
    public virtual void MoveLeft()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    public virtual void MoveRight()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }
}
