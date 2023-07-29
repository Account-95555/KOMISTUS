using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyCommon
{
    public GameObject[] points;
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
        agent.isStopped = true;
        coStart = true;
        playerPos = player.transform.position;
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = playerPos;
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        teleported = true;
        coStart = false;
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
