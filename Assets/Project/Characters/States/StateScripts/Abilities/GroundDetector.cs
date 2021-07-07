using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://www.youtube.com/watch?v=vWPEjo-EkPg,
// https://drive.google.com/drive/folders/1i8rtE2sUzaIurx-gxFutFV_C635KTw8L 
// IsGrounded umgeschrieben, Kondition für GroundCheck modifiziert.
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        [SerializeField] private float delayTime = 0.2f;
        [SerializeField] private float distanceOffset = 0.02f;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) 
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (stateInfo.normalizedTime >= delayTime)
            {
                if (IsGrounded(control))
                {
                    animator.SetBool(groundedHash, true);
                    control.distanceFallen = 0f;  
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
            float maxDistance = col.bounds.extents.y+distanceOffset;

            if (control.RIGID_BODY.velocity.y > -0.01f 
                && control.RIGID_BODY.velocity.y <= 0f)
            {
                return true;
            }   
            if ((control.RIGID_BODY.velocity.y < 0f) || 
                (!control.Jump && control.RIGID_BODY.velocity.y > 0f))
            {
                Vector3 rayOrigin = col.bounds.center + Vector3.back*(col.bounds.extents.z);
                float horizontalRayCount = 4;
                float horizontalRaySpacing = col.bounds.extents.z/2;
                for (int i=0; i<horizontalRayCount; i++) 
                {
                    RaycastHit hitInfo;
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


        new private bool IsIgnoredPart(Collider col)
        {
            switch (col.gameObject.tag)
            {
                case "Player":
                    return true;
                case "Rope":
                    return true;
                case "Ledge":
                    return true;  
                case "Ignored":
                    return true;            
                default: 
                    return false;
            }
        }
    }  
}