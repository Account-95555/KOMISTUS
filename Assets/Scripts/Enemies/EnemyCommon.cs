using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCommon : MonoBehaviour
{
    //GameObjects
    public GameObject player;
    //public GameObject 
    public GameObject enemy;
    public GameObject wearyMeter;
    public GameObject originPoint;

    //Identifier
    public string enemyType;
    //Positions


    //AudioSources
    public AudioSource chaseAudio;

    //Components
    protected PlayerController pc;
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;
    protected WearyMeter wm;


    //floats
    public float moveSpeed;

    //bools
    public bool isChasing = false;
    public bool inRange = false;
    public bool chaseCo = false;

    //Vectors
    private Vector3 target;
    private Vector3 originPos;

    // Start is called before the first frame update
    public virtual void Initialise()
    {
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        wm = wearyMeter.GetComponent<WearyMeter>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        originPos = originPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ChasePlayer()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    public virtual void GetPlayerPos()
    {
        target = player.transform.position;
        //agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        //agent.velocity = new Vector2(agent.velocity.x, agent.velocity.y);
    }

    public virtual void ReturnToPath()
    {
        agent.SetDestination(new Vector3(originPos.x, originPos.y, transform.position.z));
    }

    public virtual void ChangeScale()
    {
        enemy.transform.localScale = new Vector3(-Mathf.Sign(agent.velocity.x) * 1, 1, 1);
    }
}
