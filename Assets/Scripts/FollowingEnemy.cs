using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : EnemyCommon
{
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = currentPlayerPos.position - currentEnemyPos.position;
        movement = direction;
        direction.Normalize();
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    
    void FixedUpdate()
    {
        MoveCharacter(movement);
        /*
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1);
        currentPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        currentEnemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        Debug.Log(currentEnemyPos);
        if (enemy.transform.position.y - player.transform.position.y < 0)
        {
            MoveUp();
        }
        if (enemy.transform.position.y - player.transform.position.y > 0)
        {
            MoveDown();
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        
        if (enemy.transform.position.x - player.transform.position.x < 0)
        {
            MoveRight();
        }
        if (enemy.transform.position.x - player.transform.position.x > 0)
        {
            MoveLeft();
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        //if (playerPos > )*/
    }

}
