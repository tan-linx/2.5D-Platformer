using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        //to delay the groundCheck so it doesnt transition to fall animation in small fall heights
        private float delayTime;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            delayTime = 1f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) 
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (stateInfo.normalizedTime >= delayTime) 
            {
                if (IsGrounded(control))
                {
                    animator.SetBool(groundedHash, true);
                    return;
                }
                else 
                {
                    animator.SetBool(groundedHash, false);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }  

        /// <summary>method <c>IsGrounded</c> Checks whether Collider is grounded</summary>
        public bool IsGrounded(CharacterControl control)
        { 
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            float offset = 0.02f;  //TODO:
            float maxDistance = col.bounds.extents.y+offset;
            RaycastHit hitInfo;

            if (control.RIGID_BODY.velocity.y > -0.01f 
                && control.RIGID_BODY.velocity.y <= 0f)
            {
                return true;
            }   
            if (control.RIGID_BODY.velocity.y < 0f)
            {
                Vector3 rayOrigin = col.bounds.center + Vector3.back*(col.bounds.extents.z);
                float horizontalRayCount = 4;
                float horizontalRaySpacing = col.bounds.extents.z/2;
                for (int i=0; i<horizontalRayCount; i++) 
                {
                    if (Physics.Raycast(rayOrigin, 
                        Vector3.down, out hitInfo, maxDistance) 
                        && !IsIgnoredPart(hitInfo.collider)) 
                    {
                        return true;
                    }
                    rayOrigin += Vector3.forward*horizontalRaySpacing;
                }
            }
            return false; 
        }   
    }  
}