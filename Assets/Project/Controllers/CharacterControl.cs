using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <author Tanja Schlanstedt></author>
/* everything except for the marked methods are written by myself 
 inspirations for the architecture come from this tutorial:
 https://www.youtube.com/watch?v=n8PCkwIncwc&list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6&index=20
 https://drive.google.com/drive/folders/1R1YQbmEt-2qmGe2cvQwvU97jJHQCbziS
 */ 

namespace Platformer_Assignment
{
    public enum HitDirection 
    {
        None,
        FORWARD,
        BACK,
    }

    //Needed for the animator transitions
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private Transform spawn;

        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Crouch; 
        public bool Pull;
        public bool Push;
        public bool MoveUp;
        public bool Dead;

        public bool IsGrabbingRope;
        public bool IsClimbDownLadder;

        public float distanceFallen;

        //to retrieve information about latest collider which was hit by player
        public Collider currentHitCollider;    
        public HitDirection currentHitDirection;

        //to add velocity when player is falling 
        public float GravityMultiplier;
        public float PullMultiplier;

        private Rigidbody rigid;
        [SerializeField] private Animator animator;
        private LedgeChecker ledgeChecker;

        //For stairs handling
        [SerializeField] private GameObject stepRayUpper;
        [SerializeField] private GameObject stepRayLower;
        [SerializeField] private float stepHeight = 0.3f;
        [SerializeField] private float stepSmooth = 2f;

        void Start()
        {
            //transform.position = spawn.position;
            Dead = false;
            IsGrabbingRope = false;
            IsClimbDownLadder = false;
            distanceFallen = 0f;
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            stepRayUpper.transform.position 
                = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
        }

        void Update() 
        {
            if (Dead)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
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
    
        // based on https://drive.google.com/drive/folders/1R1YQbmEt-2qmGe2cvQwvU97jJHQCbziS
        private void FixedUpdate()
        {   
            if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier);
            }

            if (RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);
            }
        }

        // based on this tutorial https://www.youtube.com/watch?v=DrFk5Q_IwG0
        // https://github.com/DawnsCrowGames/Unity-Rigidbody_Step_Up_Stairs_Tutorial/blob/main/StairClimb.cs
        public void StepClimb(Vector3 dir)
        {                
            RaycastHit hit; 
            if (Physics.Raycast(stepRayLower.transform.position, dir, out hit, 0.1f) 
                && hit.collider.tag != "Player")
            {
                if (!Physics.Raycast(stepRayUpper.transform.position, dir, 0.2f) 
                    && hit.collider.tag != "Player")
                {
                    RIGID_BODY.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
                }
            }
        }
        
        //getters and setters
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
    }
}