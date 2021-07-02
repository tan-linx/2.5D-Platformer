using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer_Assignment
{
    public enum HitDirection {
        None,
        FORWARD,
        BACK,
    }

    //Needed for the animator transitions
    public class CharacterControl : MonoBehaviour
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Crouch; 
        public bool Pull;
        public bool Push;
        public bool MoveUp;
        public bool dead;
        public bool grabbingRope;
        public bool climbDownLadder;

        //to retrieve information about latest collider which was hit by player
        public Collider currentHitCollider;    
        public HitDirection currentHitDirection;

        //To add velocity when player is falling
        public float GravityMultiplier;
        public float PullMultiplier;
        private Rigidbody rigid;
        [SerializeField]
        private Animator animator;
        private LedgeChecker ledgeChecker;

        void Awake()
        {
            dead = false;
            grabbingRope = false;
            climbDownLadder = false;
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
        }

        void Update() 
        {
            if (dead)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

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
            else
            {
                Crouch = false;
            }  
            if (Input.GetKey(KeyCode.W)) 
            {
                MoveUp = true;
            }     
            else 
            {
                MoveUp = false;
            }
            if (Input.GetKey(KeyCode.E)) 
            {
                Pull = true;
            }     
            else 
            {
                Pull = false;
            }
            if (Input.GetKey(KeyCode.Q)) 
            {
                Push = true;
            }     
            else 
            {
                Push = false;
            }
        }

        public LedgeChecker LedgeChecker
        {
            get { return ledgeChecker; }
        }

        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
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

        /*private void FixedUpdate()
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