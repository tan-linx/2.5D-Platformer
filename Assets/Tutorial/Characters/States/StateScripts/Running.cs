using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Running")]
    public class Running : StateData
    {
        private float Speed = 5f;
        private int jumpHash;

        private CharacterControl control; 
        private Rigidbody rb;
        private Animator animator; 
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            this.animator = control.Animator;
            jumpHash = Animator.StringToHash("Jump");
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {            
            if (!CollisionChecker.IsGrounded(control))  
            {
                //Fall here
            }
            
            Debug.DrawRay(control.transform.position, 
                    Vector3.forward*0.3f, Color.red);
            //ClimbSlope();
            if (control.MoveRight)
            {                    
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!CollisionChecker.CheckFront(control))
                {
                    CheckAndDoRunningJump();
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
                }
            } 
            if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CollisionChecker.CheckFront(control))
                {
                    CheckAndDoRunningJump();
                    rb.MovePosition(rb.position+ (-Vector3.forward*Speed*Time.deltaTime));
                }     
            }
            if (control.Crouch) {
                animator.SetBool(TransitionParameter.Crouch.ToString(), true);
                return;
            }
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void CheckAndDoRunningJump() {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                animator.SetBool(jumpHash, true);
                return;
            }
        }

        float maxClimbAngle = 70;

        //ref: 
        //https://github.com/SebLague/
        //2DPlatformer-Tutorial/blob/master/Platformer%20E04/Assets/Scripts/Controller2D.cs
       /* void ClimbSlope() 
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            RaycastHit hit; //to check distance from hit
            //most Right hit or most left hit
            float gradientZ = 1f;
            Vector3 rayDir = Vector3.forward;

            if (control.MoveLeft)
            {
                rayDir = Vector3.back;
                gradientZ = -1f;
            }
            if (Physics.Raycast(control.transform.position, 
                    rayDir*collider.radius, out hit))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                Debug.Log("tag is "  +hit.collider.tag + "slope angel is " 
                + slopeAngle + "hitdistance is: " + hit.distance) ;

                float distanceToSlope = hit.distance;
                gradientZ *= distanceToSlope;

                if (hit.collider.gameObject.tag == "Slope" && slopeAngle <= maxClimbAngle)
                {    
                    float gradientY = Mathf.Tan(slopeAngle* Mathf.Deg2Rad) * Mathf.Abs(gradientZ);
                   // direction is influenced by physics
                    Vector3 dir = new Vector3(0, gradientY, gradientZ); 
                    rb.MovePosition(control.transform.position + dir * Time.deltaTime);   
                } 
            }
        }

        public void CheckCollidersInFront()
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Collider[] hitColliders = Physics.OverlapBox(
                control.transform.position, 
                new Vector3(collider.radius, collider.height, collider.radius),    
                control.transform.rotation); 
            foreach(Collider hitcollider in hitColliders) {
                switch(hitcollider.gameObject.tag) 
                {   
                    
                    case "CrouchObstacle":
                        animator.SetBool(TransitionParameter.Crouch.ToString(), true);
                        return;                        
                }
            }  
        }*/
    }
}
