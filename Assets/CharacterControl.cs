using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    //Needed for the animator transitions
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Crouch,
    }

    public class CharacterControl : MonoBehaviour
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Grounded; 
        public bool Crouch; 

        
        public float GravityMultiplier;
        public float PullMultiplier;
        private Rigidbody rigid;


        void Awake() {
        }

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }
        

        void Update() {
            if (Input.GetKey(KeyCode.D)) 
            {
                MoveRight = true;
            } 
            else 
            {
                MoveRight = false;
            }
            if (Input.GetKey(KeyCode.A)) 
            {
                MoveLeft = true;
            } 
            else 
            {
                MoveLeft = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump = true;
            } 
            else
            {
                Jump = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Crouch = true;
            } 
            if (Input.GetKey(KeyCode.W))
            {
                Crouch = false;
            }
            //else {
            //    Crouch = false; 
            //}
             
        }

       /* private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier);
            }

            if (RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);
            }
        }*/ 

    }
}