using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemy : EnemyCommon
{
    public Vector2 startingDir; //Values of startingDir should either be -1, 0, or 1
    public bool isPatrolling = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateRotation = false;
        enemyAgent.updateUpAxis = false;
        pc = player.GetComponent<PlayerController>();
        playerTransform = player.transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed * startingDir.x, moveSpeed * startingDir.y);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pc.isHiding);
        Debug.Log(playerPos);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1f);
        if (isPatrolling == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * moveSpeed,rb.velocity.y * moveSpeed);
        }
        else
        {
            playerPos = playerTransform.position;
            enemyAgent.SetDestination(new Vector3(playerPos.x, playerPos.y, transform.position.z));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && pc.isHiding == false)
        {
            pc.isDead = true;
            PlayerPrefs.SetString("CauseOfDeath", "PatrolEnemy");
        }
    }
}
