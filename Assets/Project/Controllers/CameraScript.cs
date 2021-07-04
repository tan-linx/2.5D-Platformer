using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private Transform spawn;
        [SerializeField] private Transform target;
        public Vector3 offset = new Vector3(30, 2, 0);
        public float smoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;

        void Awake()
        {
           // transform.position = spawn.position;
        }

        void Start () 
        {
        	GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1);
        }

        void Update()
        {
            //Vector3 targetedPosition = target.position + offset;
            //TODO:
           // if (target.GetComponentInParent<CharacterControl>().LedgeChecker.IsGrabbingLedge) 
 //targetedPosition = new Vector3();
            transform.LookAt(target);
            Vector3 targetedPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetedPosition, ref velocity, smoothTime);
            transform.position = smoothedPosition;
            offset = new Vector3(25, 2, 0);           
        }
    }
}   