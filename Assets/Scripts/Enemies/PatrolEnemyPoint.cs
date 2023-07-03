using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyPoint : MonoBehaviour
{
    public GameObject patrolEnemy;
    public Vector2 dirChange;
    public int PEPID;
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
            if (pe.PEID == PEPID)
            {
                rb.velocity = new Vector2(rb.velocity.x * dirChange.x, rb.velocity.y * dirChange.y);
            }
            
        }
    }
}
