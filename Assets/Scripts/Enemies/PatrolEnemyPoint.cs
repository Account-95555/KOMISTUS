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
    public float waitTime = 1f;
    public bool isWaitPoint;
    //WearyMeter wm;
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
        if (other.gameObject.GetComponent<PatrolEnemy>() != null && pe.PEID == PEPID)
        {
            if (pe.index == assignedIndex) //If the patrol enemy's index is the same as the point index
            {
                StartCoroutine(PointCo());
                
            }
            
            
            //if (pe.PEID == PEPID)
            //{
                //rb.velocity = new Vector2(rb.velocity.x * dirChange.x, rb.velocity.y * dirChange.y);
            //}
            
        }
    }
    IEnumerator PointCo() //Wait at the point if needed
    {
        yield return new WaitForSeconds(waitTime);
        if (pe.isFreeroam)
        {
            FreeroamFunc(); 
        }
        else
        {
            if (pe.index == assignedIndex)//If the patrol enemy's index is the same as the point index
            {
                pe.index += 1; //add to the patrol enemy index so it moves to the next point
            }
 
        }

    }

    public void FreeroamFunc()
    {
        if (pe.index == pe.integerStored)
        {
            while (pe.index == pe.integerStored)
            {
                pe.index = Random.Range(0, (pe.points).Length);
            }
        }
        else
        {
            pe.index = Random.Range(0, (pe.points).Length);
        }
        pe.integerStored = pe.index;
    }
}


