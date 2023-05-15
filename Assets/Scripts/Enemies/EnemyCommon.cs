using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCommon : MonoBehaviour
{
    public GameObject wearyMeter;
    public GameObject player;
    public GameObject origin;
    public PlayerController pc;
    public float moveSpeed;
    protected Vector2 playerPos;
    protected Vector2 originPos;
    protected WearyMeter wm;
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

    //For Waypointing purposes
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

    //Chase Sequence - If player's suspicion circle enters the range of the enemy or vice versa during a full weary meter, entity will seek player's position.
    public virtual void TargetPlayer()
    {
        enemyAgent.SetDestination(new Vector3(playerPos.x, playerPos.y, transform.position.z));
    }
    public virtual void GetPlayerPos()
    {
        playerPos = player.transform.position;
    }
    
    public virtual void GoOriginalPos()
    {
        enemyAgent.SetDestination(new Vector3(originPos.x, originPos.y, transform.position.z));
    }
}
