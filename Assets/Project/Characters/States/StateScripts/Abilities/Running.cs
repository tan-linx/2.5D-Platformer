using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Running")]
    public class Running : StateData
    {
        private float Speed;
        private CharacterControl control;
        private Rigidbody rb;


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            Speed = 7f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {            
            if (control.MoveRight)
            {                    
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!CheckFront(control, Vector3.forward)) 
                {
                    if(HandleColliderData(control, animator, Vector3.forward)) return;
                    if(IsJump(animator)) return;
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
                }
            } 
            if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CheckFront(control, Vector3.back))
                {
                    if (HandleColliderData(control, animator, Vector3.back)) return;
                    if(IsJump(animator)) return;
                    rb.MovePosition(rb.position+ (-Vector3.forward*Speed*Time.deltaTime));
                }     
            }
            if (control.Crouch) 
            {
                animator.SetBool(crouchHash, true);
                return;
            }
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private bool IsJump(Animator animator) 
        {
            bool isJump  = control.Jump;
            if(isJump)
            {
                animator.SetBool(jumpHash, true);
            }
            return isJump;
        }

        /// <summary>method <c>CheckColliders in Front</c> Checks if Object is on the front
        /// and does a certain Animation when a specific Tag is on the Object which it collided with.</summary>
        public bool HandleColliderData(CharacterControl control, Animator animator, Vector3 dir)
        {
            //float offset = 0.15f;
            RaycastHit hit; 
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            if (Physics.Raycast(collider.bounds.center, dir, out hit, collider.bounds.extents.z)) 
            {
                if (!IsRagdollPart(control, hit.collider))
                {
                    switch(hit.collider.tag) 
                    {   
                        case "CrouchObstacle":
                            animator.SetBool(crouchHash, true);
                            return true;      
                        case "Pushable":
                            control.currentHitDirection = dir;
                            control.currentHitCollider = hit.collider;
                            animator.SetBool(pushHash, true);
                            return true;
                        default: 
                            return false;    
                    }
                }
            }  
            return false;
        }
    }
}
