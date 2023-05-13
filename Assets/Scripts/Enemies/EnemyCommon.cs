using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCommon : MonoBehaviour
{
    public GameObject player;
    public PlayerController pc;
    public float moveSpeed;
    protected Transform playerTransform;
    protected Vector2 playerPos;
    protected Rigidbody2D rb;
    protected NavMeshAgent enemyAgent;
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
