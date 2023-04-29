using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Transform currentPlayerPos;
    public Transform currentEnemyPos;
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isTargeting;
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
        rb.velocity = new Vector2(rb.velocity.x, 5f);
    }
    public virtual void MoveDown()
    {
        rb.velocity = new Vector2(rb.velocity.x, -5f);
    }
    public virtual void MoveLeft()
    {
        rb.velocity = new Vector2(-5f, rb.velocity.y);
    }
    public virtual void MoveRight()
    {
        rb.velocity = new Vector2(5f, rb.velocity.y);
    }

    //Pathfind

}
