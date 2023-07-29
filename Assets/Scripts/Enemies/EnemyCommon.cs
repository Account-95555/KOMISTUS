using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCommon : MonoBehaviour
{
    //GameObjects
    public GameObject player;
    public GameObject deathParticles;
    public GameObject sigil;
    public GameObject entity;
    public GameObject entityHolder;
    //public GameObject 
    public GameObject enemy;
    public GameObject wearyMeter;
    //public GameObject originPoint;
    public GameObject fovObject;

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
    protected FOV fov;


    //floats
    public float moveSpeed;
    public float range;

    //bools
    public bool isChasing = false;
    public bool inRange = false;
    public bool chaseCo = false;

    //Vectors
    private Vector3 target;
    //private Vector3 originPos;

    // Start is called before the first frame update
    public virtual void Initialise()
    {
        pc = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        wm = wearyMeter.GetComponent<WearyMeter>();
        fov = fovObject.GetComponent<FOV>();
        moveSpeed = agent.speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //originPos = originPoint.transform.position;
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

    //public virtual void ReturnToPath()
    //{
        //agent.SetDestination(new Vector3(originPos.x, originPos.y, transform.position.z));
    //}

    public virtual void ChangeScale()
    {
        enemy.transform.localScale = new Vector3(-Mathf.Sign(agent.velocity.x) * 1, 1, 1);
    }

    public virtual IEnumerator SealBreak()
    {

        deathParticles.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y, deathParticles.transform.position.z);
        sigil.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y, deathParticles.transform.position.z);
        yield return new WaitForSecondsRealtime(1.1f);
        sigil.SetActive(true);
        //entity.SetActive(false);
        yield return new WaitForSecondsRealtime(1.1f);
        sigil.SetActive(false);
        deathParticles.SetActive(true);
        yield return new WaitForSecondsRealtime(2.1f);
        entityHolder.SetActive(false);
    }
}
