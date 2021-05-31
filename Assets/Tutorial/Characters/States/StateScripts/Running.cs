using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Running")]
    public class Running : StateData
    {
        private float Speed = 2f;
        private int jumpHash;

        private CharacterControl control; 
        private Rigidbody rb;
        private Animator anim; 
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            anim = control.GetComponent<Animator>();
            jumpHash = Animator.StringToHash("Jump");
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {            
            //TODO now
           // CheckCollidersInFront();
            if (control.MoveRight)
            {                    
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!CollisionChecker.CheckFront(control))
                {
                    CheckAndDoRunningJump();
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
                }
            } 
            else if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CollisionChecker.CheckFront(control))
                {
                    CheckAndDoRunningJump();
                    rb.MovePosition(rb.position+ (-Vector3.forward*Speed*Time.deltaTime));
                }     
            }
            else if (control.Crouch)
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), true);
                return;
            }
            else 
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool CheckCollidersInFront()
        {
             //when rigid_body is falling
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Collider[] hitColliders = Physics.OverlapBox(
                control.transform.position, 
                new Vector3(collider.radius, collider.height, collider.radius),    
                control.transform.rotation); //maybe add frontlayermask
            Debug.DrawRay(control.transform.position, Vector3.forward*collider.transform.position.z, Color.green);
            Debug.Log("OverlapBox picked up this many colliders: " + hitColliders.Length);
            /**
            extend the loop here 
            **/ 
            foreach(Collider hitcollider in hitColliders) {
                switch(hitcollider.gameObject.tag) 
                {   
                    
                    case "CrouchObstacle":
                        //control.Crouch = true;
                        return true;
                }
            }  
            return false;
        }

        private void CheckAndDoRunningJump() {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                anim.SetBool(jumpHash, true);
                return;
            }
        }
    }
}
