using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/CrouchWalk")]
    public class CrouchWalk: StateData
    {
        private float Speed;
        private CharacterControl control;
        private Rigidbody rb;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            Speed =  2f;    
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!control.Crouch && !CheckHead())
            {
                animator.SetBool(crouchHash, false);
                return;
            }   
            if (control.MoveRight)
            {                    
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!CheckFront(control, Vector3.forward))
                {
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);

                }
            } 
            if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CheckFront(control, Vector3.back))
                {
                    rb.MovePosition(rb.position+(-Vector3.forward*Speed*Time.deltaTime));
                }     
            }
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        /// <summary>method <c>CheckHead</c> Checks if Collider collides with something up</summary>
        public bool CheckHead()
        {  
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = collider.bounds.center; 
            Vector3 dir = Vector3.up;
            RaycastHit hitInfo;
            float maxRayLength = collider.bounds.extents.y;
            //Debug.DrawRay(rayOrigin, dir, Color.green);
            if(Physics.Raycast(rayOrigin,  dir, out hitInfo, maxRayLength) && !IsRagdollPart(control, hitInfo.collider))
                return true; 
            return false;    
        } 

        /// <summary>method <c>CheckFront</c> Overrides CheckFront from inherited Class because 
        /// it is different here.</summary>
        new private bool CheckFront(CharacterControl control, Vector3 dir)
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            float maxRayLength = collider.bounds.size.z;
            if(Physics.Raycast(collider.bounds.center, dir, maxRayLength*2))
                return true;
            return false;
        }
    }
}