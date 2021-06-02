using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    void Start() 
    {
        offset = new Vector3(13, 2, 0);
    }

    void Update()
    {
        transform.LookAt(target);
        Vector3 targetedPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetedPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }
}
