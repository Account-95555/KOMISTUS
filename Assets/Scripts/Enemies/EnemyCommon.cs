using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    //GameObjects
    public GameObject player;
    public GameObject enemy;

    //Identifier
    public string enemyType;
    //Positions

    //Components
    protected PlayerController pc;
    protected Rigidbody2D rb;

    //floats
    public float moveSpeed;

    //bools
    public bool isChasing = false;
    public bool inRange = false;

    // Start is called before the first frame update
    public virtual void Initialise()
    {
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
