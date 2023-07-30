using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyCommon
{
    public GameObject[] points;
    public GameObject appearParticles; //if not aizen, leave blank
    public GameObject drip;
    public InteractableV2 sourceObject;
    public Vector2 playerPos;
    public Vector2 initialDir;
    public int PEID;
    public bool isOriginEntity;
    public bool isFreeroam;
    public bool isNormal = true;
    public bool isMinion;
    public bool isAizen;
    public bool teleported;
    public AudioClip aizenGrowl;
    private bool coStart = false;
    //public int integerStored = 0;
    public int index = 0;

    private bool disintegrating;

    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        rb.velocity = new Vector2(initialDir.x * moveSpeed, initialDir.y * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity == Vector3.zero)
        {
            if (isAizen)
            {
                if (coStart)
                {
                    anim.SetBool("idle", false);
                }
                else
                {
                    anim.SetBool("idle", true);
                }
            }
            else
            {
                anim.SetBool("idle", true);
            }
            
        }
        else
        {
            anim.SetBool("idle", false);
        }
        if (sourceObject.sourceDestroyed && !isOriginEntity && !disintegrating)
        {
            StartCoroutine(SealBreak());
            disintegrating = true; //Prevent coroutine from running again

        }
        transform.position  = new Vector3(transform.position.x,transform.position.y,0);
        ChangeScale();
        if (wm.canBeChased == true)
        {
            if (isNormal)
            {
                anim.SetBool("chasing", isChasing);
                agent.speed = moveSpeed * 1.25f;
                DisablePoints();
                GetPlayerPos();
                ChasePlayer();
                isChasing = true;
            }
            else if (isAizen)
            {
                if (!coStart && !teleported)
                {
                    StartCoroutine(AizenSpawn());
                }
                if (teleported)
                {
                    DisablePoints();
                    GetPlayerPos();
                    ChasePlayer();
                    isChasing = true;
                }

            }
            
        }
        else if (wm.canBeChased == false /* && !chaseCo && isChasing*/)
        {
            //GetPlayerPos();
            //ChasePlayer();
            //StartCoroutine(EndChase());
            EndChase();
        }
        /*else
        {
            if (isNormal)
            {
                agent.speed = moveSpeed;
                GoToPosition();
            }
        }*/


        //Debug.Log(hit);
        Debug.DrawRay(transform.position, player.transform.position - transform.position);

        if (hit.collider != null)
        {
            if (fov.isInFOV && pc.isHiding == false)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (isNormal || isAizen)
                    {
                        wm.wearyVal += 15f * Time.deltaTime;
                    }
                    else if (isMinion)
                    {

                    }
                    //Debug.Log("Perchance");
                }
            }
        }
    }

    void FixedUpdate()
    {
        //Vector2 v2TEnemy = new Vector2(transform.position.x, transform.position.y);
        //Vector2 v2TPlayer = new Vector2(player.transform.position.x, player.transform.position.y);
        hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
    }

    IEnumerator AizenSpawn()
    {
        agent.isStopped = true; //stop AIZEN from moving
        coStart = true; //coroutine has started
        playerPos = player.transform.position; //get the player position
        yield return new WaitForSeconds(2f);
        appearParticles.SetActive(true);
        drip.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = false; //disable the kill radius since AIZEN is emerging from the ground
        gameObject.transform.position = playerPos; //emerge at the location the player was 2 seconds earlier
        anim.SetBool("appear", true);
        yield return new WaitForSeconds(1f);
        //appearParticles.SetActive(false);
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        drip.SetActive(true);
        anim.SetBool("appear", false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("chasing", true);
        agent.isStopped = false; //AIZEN is free to roam around now
        teleported = true;
        coStart = false;
        wm.wearyVal += 10f;
        chaseAudio.PlayOneShot(aizenGrowl);
        gameObject.GetComponent<BoxCollider2D>().enabled = true; //reenable the kill radius since he is fully emerged
    }
    void GoToPosition()
    {
        if (index >= (points.Length))
        {
            index = 0;
        }
        agent.SetDestination(new Vector3(points[index].transform.position.x, points[index].transform.position.y, transform.position.z));
    }

    /*IEnumerator EndChase()
    {
        yield return new WaitForSeconds(6.9f);
        if (wm.canBeChased == false)
        {
            chaseCo = true;
            isChasing = false;
            EnablePoints();
            GoToPosition();
            chaseAudio.Stop();
            chaseCo = false;
        }

    }*/
    void EndChase()
    {
        EnablePoints();
        GoToPosition();
        teleported = false;
    }

    void DisablePoints()
    {
        foreach (GameObject point in points)
        {
            point.SetActive(false);
        }
    }
    void EnablePoints()
    {
        foreach (GameObject point in points)
        {
            point.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!pc.isHiding)
            {
                pc.isDead = true;
            }
            
        }
    }

    

}
