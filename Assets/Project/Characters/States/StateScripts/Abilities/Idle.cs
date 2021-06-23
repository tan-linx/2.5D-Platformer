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
                animator.SetBool(moveHash, true);
                return; 
            } 
            if (control.MoveLeft)
            {
                animator.SetBool(moveHash, true);
                return;
            }
            if (GetColliderTag() == "Rope" && control.currentHitCollider.attachedRigidbody.velocity.y < 3f)  //control.Moveup
            {
                animator.SetBool("Hanging", true);
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
            if (control.MoveLeft) dir = Vector3.back;
            Debug.DrawRay(control.transform.position+Vector3.up*(collider.height/2), dir*collider.radius, Color.yellow);
            //Gizmos.DrawSphere(collider.bounds.center+Vector3.up*(collider.bounds.extents.y/2), collider.bounds.extents.z); 
            if (Physics.SphereCast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y), 
            collider.bounds.extents.z, dir, out hit, collider.bounds.extents.z))
            //if(Physics.Raycast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y/2), dir, out hit, collider.bounds.extents.z)) 
            { 
                if (!IsRagdollPart(control, hit.collider))
                {
                    control.currentHitCollider = hit.collider;
                    return hit.collider.gameObject.tag;
                }
            }
            return "";
        }
    }
}