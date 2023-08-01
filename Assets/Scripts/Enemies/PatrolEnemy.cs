using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrolEnemy : EnemyCommon
{
    public GameObject[] points;
    public GameObject appearParticles; //if not aizen, leave blank
    public GameObject drip;
    public Slider minionSlider;
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
    public bool screeching;
    public bool aizenInRange;
    public AudioClip aizenGrowl;
    public AudioSource minionScreech;
    private bool coStart = false;
    //public int integerStored = 0;
    public int index = 0;

    private bool disintegrating;
    private bool alreadyChasing;
    

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
        
        if (isMinion)
        {
            minionSlider.value = Mathf.Clamp(minionSlider.value, 0f, 100f);
            minionSlider.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.75f, gameObject.transform.position.z);
            if (screeching)
            {
                wm.wearyVal += 42f * Time.deltaTime;
            }
            if (minionSlider.value >= 100f)
            {
                minionSlider.value = 100f;
                screeching = true;
                if (!minionScreech.isPlaying)
                {
                    minionScreech.Play();
                }
                anim.SetBool("screeching", screeching);
                agent.isStopped = true;
            }
            else if (minionSlider.value <= 50f)
            {
                if (minionScreech.isPlaying)
                {
                    minionScreech.Stop();
                }
                screeching = false;
                agent.isStopped = false;
                EndChase();
                anim.SetBool("screeching", screeching);
            }
        }

        if (isAizen)
        {
            if (gameObject.GetComponent<SpriteRenderer>().isVisible)
            {
                aizenInRange = true;
            }
            else
            {
                aizenInRange = false;
            }
        }

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
            else if (isMinion)
            {
                if (screeching)
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
                if (!aizenInRange)
                {
                    if (!coStart && !teleported && !alreadyChasing)
                    {
                        StartCoroutine(AizenSpawn());
                    }
                    else if (teleported)
                    {
                        anim.SetBool("chasing", true);
                        agent.speed = moveSpeed * 1.25f;
                        DisablePoints();
                        GetPlayerPos();
                        ChasePlayer();
                        isChasing = true;
                    }
                }
                else
                {
                    alreadyChasing = true;
                    anim.SetBool("inRange", aizenInRange);
                    anim.SetBool("chasing", true);
                    agent.speed = moveSpeed * 1.25f;
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
        else
        /*{
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
                        minionSlider.value += 84f * Time.deltaTime;
                    }
                    //Debug.Log("Perchance");
                }
            }
            else if (fov.isInFOV == false || pc.isHiding)
            {
              
                if (isMinion)
                {
                    minionSlider.value -= 20f * Time.deltaTime;
                
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
        
        coStart = true; //coroutine has started
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z); //get the player position
        yield return new WaitForSeconds(1f);
        appearParticles.SetActive(true);
        drip.SetActive(false);
        agent.Warp(playerPos);
        agent.isStopped = true; //stop AIZEN from moving
        gameObject.GetComponent<BoxCollider2D>().enabled = false; //disable the kill radius since AIZEN is emerging from the ground
        //gameObject.transform.position = playerPos; //emerge at the location the player was 2 seconds earlier
        anim.SetBool("appear", true);
        yield return new WaitForSeconds(1f);
        //appearParticles.SetActive(false);
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        drip.SetActive(true);
        anim.SetBool("appear", false);
        yield return new WaitForSeconds(1f);
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
        agent.speed = moveSpeed;
        EnablePoints();
        GoToPosition();
        teleported = false;
        isChasing = false;
        alreadyChasing = false;
        if (!isMinion)
        {
            anim.SetBool("chasing", isChasing);
        }
        if (isAizen)
        {
            anim.SetBool("inRange", aizenInRange);
        }
        
    }

    void DisablePoints()
    {
        foreach (GameObject point in points)
        {
            if (point.activeSelf) //so it doesn't run infinitely
            {
                point.SetActive(false);
            }
            
        }
    }
    void EnablePoints()
    {
        foreach (GameObject point in points)
        {
            if (!point.activeSelf)
            {
                point.SetActive(true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!pc.isHiding && !isMinion)
            {
                pc.isDead = true;
            }
            
        }
    }

    

}
