using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To use this script, attach it to the GameObject that you would like to rotate towards another game object.
// After attaching it, go to the inspector and drag the GameObject you would like to rotate towards into the target field.
// Move the target around in the scene view to see the GameObject continuously rotate towards it.
public class RotateTowardsTest : MonoBehaviour
{
    public Transform target;
    public float turnSpeed = 100f;
    void Update()
    {
        Vector3 current = transform.forward;
        Vector3 to = target.position - transform.position;
        transform.forward = Vector3.RotateTowards(current, to, turnSpeed * Time.deltaTime, 360f);
    }


}
