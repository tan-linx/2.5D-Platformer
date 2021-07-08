using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
/// <summary>Class <c>TriggerPhysicsOff</c>
/// If a another gameObject hits the gameobject that has this script attached their
/// rigidbody gets removed.</summary>
public class TriggerPhysicsOff : MonoBehaviour
{
    private Collider col; 
    private float startTime;
    private float delayTime = 4f;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Pushable")
        {
            startTime = Time.time;
            this.col = col;
        }
    }

    private void Update()
    {
        if (col && (Time.time-startTime) >= delayTime)
        {
            if (col.attachedRigidbody) Destroy(col.attachedRigidbody);
        }
    }
}
