using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : EnemyCommon
{
    public Vector2 startingDir; //Values of startingDir should either be -1, 0, or 1
    protected bool isPatrolling = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed * startingDir.x, moveSpeed * startingDir.y);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1f);
        if (isPatrolling == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * moveSpeed,rb.velocity.y * moveSpeed);
        }
    }
}
