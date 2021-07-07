using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://www.youtube.com/watch?v=MFQhpwc6cKE
namespace Platformer_Assignment
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private Transform spawn;
        [SerializeField] private Transform target;
        public Vector3 offset;
        public float smoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;

        void Awake()
        {
            offset = new Vector3(32, 2, 0);
           // transform.position = spawn.position;
        }

        void Update()
        {
            transform.LookAt(target);
            Vector3 targetedPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetedPosition, ref velocity, smoothTime);
            transform.position = smoothedPosition;
        }
    }
}   