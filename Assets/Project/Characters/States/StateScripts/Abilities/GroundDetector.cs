using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basiert auf https://www.youtube.com/watch?v=vWPEjo-EkPg,
// https://drive.google.com/drive/folders/1i8rtE2sUzaIurx-gxFutFV_C635KTw8L 
// IsGrounded umgeschrieben, Kondition für GroundCheck modifiziert.
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        //to delay the groundCheck so it doesnt transition to fall animation in small fall heights
        [SerializeField] private float delayTime = 0.5f;
        //[SerializeField] private float maxDistance;
        private float firstTime;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            firstTime = Time.time;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) 
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            float timeElapsed = Time.time - firstTime;
            if (!control.IsClimbingStep &&  timeElapsed >= delayTime) 
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
        /// <author Tanja Schlanstedt></author>
        public bool IsGrounded(CharacterControl control)
        { 
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            float offset = 0.02f;  
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
                 /*if (Physics.Raycast(col.bounds.center, 
                        Vector3.down, out hitInfo, maxDistance) 
                        && !IsIgnoredPart(hitInfo.collider))
                        //&& hitInfo.collider.tag != "Rope"
                        //&& hitInfo.collider.tag != "Ledge") 
                {
                    return true;
                }*/
            }
            return false; 
        }   
    }  
}