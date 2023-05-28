using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyCommon
{
    public GameObject[] points;
    public Vector2 initialDir;
    public int PEID;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        rb.velocity = new Vector2(initialDir.x * moveSpeed, initialDir.y * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
        if (wm.canBeChased == true)
        {
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
            GoToPosition();
        }
       
        
    }

    void GoToPosition()
    {
        if (index == (points.Length-1))
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
}
