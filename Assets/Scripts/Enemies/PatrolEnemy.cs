using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyCommon
{
    public GameObject[] points;
    public Vector2 initialDir;
    public int PEID;
    public bool isFreeroam;
    //public int integerStored = 0;
    public int index = 0;

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
        transform.position  = new Vector3(transform.position.x,transform.position.y,0);
        ChangeScale();
        if (wm.canBeChased == true)
        {
            agent.speed = moveSpeed * 1.25f;
            DisablePoints();
            GetPlayerPos();
            ChasePlayer();
            isChasing = true;
        }
        else if (wm.canBeChased == false && !chaseCo && isChasing)
        {
            GetPlayerPos();
            ChasePlayer();
            StartCoroutine(EndChase());
        }
        else
        {
            agent.speed = moveSpeed;
            GoToPosition();
        }

        
        //Debug.Log(hit);
        Debug.DrawRay(transform.position, player.transform.position - transform.position);

        if (hit.collider != null)
        {
            if (fov.isInFOV && pc.isHiding == false)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    wm.wearyVal += 15f * Time.deltaTime;
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

    void GoToPosition()
    {
        if (index >= (points.Length))
        {
            index = 0;
        }
        agent.SetDestination(new Vector3(points[index].transform.position.x, points[index].transform.position.y, transform.position.z));
    }

    IEnumerator EndChase()
    {
        yield return new WaitForSeconds(6.9f);
        if (wm.canBeChased == false)
        {
            chaseCo = true;
            chaseAudio.Stop();
            isChasing = false;
            EnablePoints();
            GoToPosition();
            chaseCo = false;
        }

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

    void OnTriggerEnter2D(Collider2D other)
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
