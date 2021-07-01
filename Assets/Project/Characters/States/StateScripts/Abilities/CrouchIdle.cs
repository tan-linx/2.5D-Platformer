using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/CrouchIdle")]
    public class CrouchIdle : StateData
    {
        
        private CharacterControl control;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!control.Crouch)
            {
                animator.SetBool(crouchHash, false);
                return;
            }   
            if (control.MoveLeft || control.MoveRight)
            {
                animator.SetBool(moveHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        /// <summary>method <c>CheckHead</c> Checks if Collider collides with something up</summary>
        public bool CheckHead(CharacterControl control)
        {  
            RaycastHit hitInfo;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = collider.bounds.center; 
            Vector3 dir = Vector3.up;
            float maxRayLength = collider.bounds.extents.y;
            //Debug.DrawRay(rayOrigin, dir, Color.green);
            if(Physics.Raycast(rayOrigin,  dir, out hitInfo, maxRayLength) && !IsRagdollPart(control, hitInfo.collider))
                return true; 
            return false;    
        } 
    }
}