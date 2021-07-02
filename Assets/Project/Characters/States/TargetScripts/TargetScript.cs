using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


namespace Platformer_Assignment {
    public class TargetScript : MonoBehaviour
    {
        [SerializeField]
        private CharacterControl control;
        private float force; 

        void Start()
        {
        }

        void Update()
        {
            if (control.grabbingRope && control.currentHitCollider!=null)
            {
                force = control.RIGID_BODY.velocity.z;
                Vector3 dir; 
                if (force > 0) dir = Vector3.back;
                else if (force == 0) dir = Vector3.zero;
                else dir = Vector3.forward;
                transform.position = transform.position+dir*Time.deltaTime;
            }
        }
    }
}   