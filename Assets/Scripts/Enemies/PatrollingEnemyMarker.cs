using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyMarker : PatrollingEnemy
{
    public GameObject enemy;
    public string dirChange;
    // Start is called before the first frame update
    void Start()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PatrollingEnemy>() != null && isPatrolling == true)
        {
            if (dirChange == "up")
            {
                MoveUp();
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            if (dirChange == "down")
            {
                MoveDown();
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            if (dirChange == "left")
            {
                MoveLeft();
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            if (dirChange == "right")
            {
                MoveRight();
                rb.velocity = new Vector2(rb.velocity.x, 0);

            }
        }
        
    }
}
