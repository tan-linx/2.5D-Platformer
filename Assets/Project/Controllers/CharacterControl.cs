using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    //Needed for the animator transitions
    public class CharacterControl : MonoBehaviour
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Crouch; 
        public bool Pull;
        public bool MoveUp;
        private bool dead;
        public bool grabbingRope;
        public bool climbDownLadder;

        //to retrieve information about latest collider which was hit by player
        public Collider currentHitCollider;    
        public Vector3 currentHitDirection;
 
        //swimming
        public bool isSwimming;

        //To add velocity when player is falling
        public float GravityMultiplier;
        public float PullMultiplier;
        private Rigidbody rigid;
        [SerializeField]
        private Animator animator;
        private LedgeChecker ledgeChecker;
        private List<Collider> ragdollParts = new List<Collider>();

        void Awake()
        {
            dead = false;
            grabbingRope = false;
            climbDownLadder = false;

            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            InitializeRagdollColliders();
        }

        void Update() 
        {
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

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public List<Collider> RagdollParts
        {
            get { return ragdollParts; }
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

        private void FixedUpdate()
        {   
            /*if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier);
            }

            if (RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);
            }*/
        }

        public void InitializeRagdollColliders() 
        {
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
            foreach(Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    ragdollParts.Add(c);
                }
            }
        }

        public void TurnOnRagdoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            animator.enabled = false;
            animator.avatar = null;
            foreach(Collider c in ragdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}