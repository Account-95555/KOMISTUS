using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyPoint : MonoBehaviour
{
    public GameObject patrolEnemy;
    public GameObject wearyMeter;
    public Vector2 dirChange;
    public int assignedIndex;
    public int PEPID;
    public int waitTime;
    public bool isWaitPoint;
    WearyMeter wm;
    Rigidbody2D rb;
    PatrolEnemy pe;
    // Start is called before the first frame update
    void Start()
    {
        rb = patrolEnemy.GetComponent<Rigidbody2D>();
        pe = patrolEnemy.GetComponent<PatrolEnemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PatrolEnemy>() != null)
        {
            if (pe.index == assignedIndex)
            {
                if (isWaitPoint)
                {
                    StartCoroutine(Wait());
                }
                else
                {
                    pe.index += 1;
                }
            }
            
            
            //if (pe.PEID == PEPID)
            //{
                //rb.velocity = new Vector2(rb.velocity.x * dirChange.x, rb.velocity.y * dirChange.y);
            //}
            
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        pe.index += 1;

    }
}


