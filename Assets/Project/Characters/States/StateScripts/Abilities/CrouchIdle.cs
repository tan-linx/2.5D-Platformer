using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/CrouchIdle")]
    public class CrouchIdle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (!control.Crouch && !CheckHead(control))
            {
                animator.SetBool(crouchHash, false);
                return;
            }   
            if ((control.MoveLeft && !CrouchCheckFront(control, Vector3.back))||
                (control.MoveRight && !CrouchCheckFront(control, Vector3.forward)))
            {
                animator.SetBool(moveHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        /// <summary>method <c>CheckHead</c> Checks if Collider collides with something up</summary>
        public bool CheckHead(CharacterControl control)
        {  
            RaycastHit hitInfo;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = collider.bounds.center; 
            Vector3 dir = Vector3.up;
            float maxRayLength = collider.bounds.extents.y;
            if(Physics.Raycast(rayOrigin,  dir, out hitInfo, maxRayLength))
                return true; 
            return false;    
        } 

        ///<summary>method <c>CheckFront</c> Overrides CheckFront from inherited Class because 
        ///it is different here.</summary>
        private bool CrouchCheckFront(CharacterControl control, Vector3 dir)
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            float maxRayLength = collider.bounds.size.z;
            if(Physics.Raycast(collider.bounds.center, dir, maxRayLength*2+0.1f))
                return true;
            return false;
        }
    }
}