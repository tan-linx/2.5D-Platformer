using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Idle")]
    public class Idle : StateData
    {
        private CharacterControl control; 
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            animator.SetBool(crashHash, false);
            animator.SetBool(jumpHash, false);
            animator.SetBool(moveHash, false);
            animator.SetBool(crouchHash, false);
            animator.SetBool(pushHash, false);  
            animator.SetBool(hangingHash, false);    
            animator.SetBool(transitionHash, false);  
            animator.SetBool(groundedHash, true);  //TODO might be false
            animator.SetBool(climbHash, false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.Jump)
            {
                animator.SetBool(jumpHash, true);
                return;
            } 
            if (control.Crouch)
            {
                //Ladder
                if (control.climbDownLadder) 
                {
                    animator.SetBool(climbHash, true);
                    return;
                }
                animator.SetBool(crouchHash, true);
                return;
            }
            if (control.MoveRight)
            {
                if (HandleColliderData(control, animator, Vector3.forward, 0.2f) == "Pushable")
                {
                    animator.SetBool(pushHash, true);
                    return;
                }
                animator.SetBool(moveHash, true);
                return; 
            } 
            if (control.MoveLeft)
            {
                if (HandleColliderData(control, animator, Vector3.back, 0.2f) == "Pushable")
                {
                    animator.SetBool(pushHash, true);
                    return;
                }
                animator.SetBool(moveHash, true);
                return;
            }
            
            if (control.Pull && (HandleColliderData(control, animator, Vector3.forward, 0.25f) == "Pushable" 
                                || HandleColliderData(control, animator, Vector3.back, 0.25f) == "Pushable")) 
            {
                animator.SetBool("Pull", true);
                return;
            }
            //TODO:
            if (control.MoveUp && (HandleColliderData(control, animator, Vector3.forward, 0.3f) == "Ladder" 
                || HandleColliderData(control, animator, Vector3.back, 0.3f) == "Ladder"))
            {               
                animator.SetBool(climbHash, true);
                return;
            }
            if (control.MoveUp && GetColliderTag(control, Vector3.forward) == "Rope" && control.currentHitCollider.attachedRigidbody.velocity.y < 3f)  
            {
                animator.SetBool(hangingHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {}

        /// <summary>method <c>CheckColliders in Front</c> Checks if Object is on the front</summary>
        private string HandleColliderData(CharacterControl control, Animator animator, Vector3 dir, float offset)
        {
            RaycastHit hit; 
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            if (Physics.Raycast(collider.bounds.center, dir, out hit, collider.bounds.extents.z+offset)) 
            {
                if (!IsRagdollPart(control, hit.collider))
                {
                    switch(hit.collider.tag) 
                    {   
                        //case "CrouchObstacle":
                        //    animator.SetBool(crouchHash, true);
                        //    return true;      
                        case "Pushable": //TODO: rename to Moveable                          
                            control.currentHitDirection = dir;
                            control.currentHitCollider = hit.collider;
                            break;
                        case "Ladder":
                            control.currentHitDirection = dir;
                            control.currentHitCollider = hit.collider;
                            break;    
                        default: 
                            return "";    
                    }
                    return hit.collider.tag;
                }
            }  
            return "";
        }
    }
}