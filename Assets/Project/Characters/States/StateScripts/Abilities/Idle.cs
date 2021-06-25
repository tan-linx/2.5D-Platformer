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
                animator.SetBool(crouchHash, true);
                return;
            }
            if (control.MoveRight)
            {
                if (HandleColliderData(control, animator, Vector3.forward, 0.2f))
                {
                    animator.SetBool(pushHash, true);
                    return;
                }
                animator.SetBool(moveHash, true);
                return; 
            } 
            if (control.MoveLeft)
            {
                if (HandleColliderData(control, animator, Vector3.back, 0.2f))
                {
                    animator.SetBool(pushHash, true);
                    return;
                }
                animator.SetBool(moveHash, true);
                return;
            }
            if (control.Pull && (HandleColliderData(control, animator, Vector3.forward, 0.25f) 
                                || HandleColliderData(control, animator, Vector3.back, 0.25f))) 
            {
                Debug.Log("I reached the pull state");
                animator.SetBool("Pull", true);
                return;
            }
            if (GetColliderTag() == "Rope" && control.currentHitCollider.attachedRigidbody.velocity.y < 3f)  //control.Moveup
            {
                animator.SetBool(hangingHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private string GetColliderTag() 
        {
            RaycastHit hit;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 dir = Vector3.forward;
            if (Physics.SphereCast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y), 
            collider.bounds.extents.z, dir, out hit, collider.bounds.extents.z))
            { 
                if (!IsRagdollPart(control, hit.collider))
                {
                    control.currentHitCollider = hit.collider;
                    return hit.collider.gameObject.tag;
                }
            }
            return "";
        }

        /// <summary>method <c>CheckColliders in Front</c> Checks if Object is on the front</summary>
        private bool HandleColliderData(CharacterControl control, Animator animator, Vector3 dir, float offset)
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