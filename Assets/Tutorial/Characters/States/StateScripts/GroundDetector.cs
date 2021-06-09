using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    this class checks whether Character is Grounded 
**/ 
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        private float maxDistance;
        private CharacterControl control;
        private Rigidbody rb;
        private CapsuleCollider collider;
        private RaycastHit hitInfo;


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            collider = control.GetComponent<CapsuleCollider>();
            float offset = 0.05f; 
            maxDistance = collider.height/2 + offset;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) 
        {
            Debug.Log("Is Player grounded" + IsGrounded());
            if (IsGrounded())
            {
                animator.SetBool(groundedHash, true);
            }
            else 
            {
                animator.SetBool(groundedHash, false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }  

        /// <summary>method <c>IsGrounded</c> Checks whether Collider is grounded</summary>
        public bool IsGrounded()
        { 
            if (control.RIGID_BODY.velocity.y > -0.001f 
                && control.RIGID_BODY.velocity.y <= 0f)
            {
                return true;
            }   
            if (control.RIGID_BODY.velocity.y < 0f)
            {
                Vector3 rayOrigin = control.transform.position + Vector3.back*collider.radius;
                float horizontalRayCount = 4;
                float horizontalRaySpacing = collider.radius/2;

                for (int i=0; i<horizontalRayCount; i++) 
                {
                    Debug.DrawRay(rayOrigin, 
                        Vector3.down*maxDistance, Color.green);
                    if (Physics.Raycast(rayOrigin, 
                        Vector3.down, out hitInfo, maxDistance)) 
                    {
                        return true;
                    }
                   // maxDistance = hitInfo.distance; 
                    rayOrigin += Vector3.forward*horizontalRaySpacing;
                }
            }
            return false; 
        }   
    }  
}