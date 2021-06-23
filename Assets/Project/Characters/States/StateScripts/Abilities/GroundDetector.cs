using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        [SerializeField]
        private float CheckTime;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CheckTime = 0.1f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) 
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (stateInfo.normalizedTime >= CheckTime) {
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
            animator.SetBool(groundedHash, true);
        }  

        /// <summary>method <c>IsGrounded</c> Checks whether Collider is grounded</summary>
        public bool IsGrounded(CharacterControl control)
        { 
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            float offset = 0.002f; 
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
                float horizontalRaySpacing = col.bounds.extents.z;
                for (int i=0; i<horizontalRayCount; i++) 
                {
                    //Debug.DrawRay(rayOrigin, Vector3.down*maxDistance, Color.green);
                    if (Physics.Raycast(rayOrigin, 
                        Vector3.down, out hitInfo, maxDistance) && !IsRagdollPart(control, hitInfo.collider)) 
                    {
                        return true;
                    }
                    rayOrigin += Vector3.forward*horizontalRaySpacing;
                }
            }
            return false; 
        }   

        private bool OverWrites(Animator animator) 
        {
            Debug.Log(animator.GetBool(hangingHash));
            return animator.GetBool(hangingHash);
        }
    }  
}