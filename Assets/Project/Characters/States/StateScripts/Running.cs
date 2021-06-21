﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Running")]
    public class Running : StateData
    {
        private float Speed;

        private CharacterControl control;
        private Rigidbody rb;
        private Animator animator;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            this.animator = control.Animator;
            Speed = 7f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {            
            if (control.MoveRight)
            {                    
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (CheckFront(control)) 
                {
                    HandleColliderData(control, animator);
                    return;
                }
                else
                {
                    if(IsJump()) return;
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
                }
            } 
            if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (CheckFront(control))
                {
                    HandleColliderData(control, animator);
                    return;
                }
                else 
                {
                    if(IsJump()) return;
                    rb.MovePosition(rb.position+ (-Vector3.forward*Speed*Time.deltaTime));
                }     
            }
            if (control.Crouch) 
            {
                animator.SetBool(crouchHash, true);
                return;
            }
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private bool IsJump() 
        {
            bool isJump  = control.Jump;
            if(isJump)
            {
                animator.SetBool(jumpHash, true);
            }
            return isJump;
        }
        //ref: 
        //https://github.com/SebLague/
        //2DPlatformer-Tutorial/blob/master/Platformer%20E04/Assets/Scripts/Controller2D.cs
       /* void ClimbSlope() 
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            RaycastHit hit; //to check distance from hit
            //most Right hit or most left hit
            float gradientZ = 1f;
            Vector3 rayDir = Vector3.forward;

            if (control.MoveLeft)
            {
                rayDir = Vector3.back;
                gradientZ = -1f;
            }
            if (Physics.Raycast(control.transform.position, 
                    rayDir*collider.radius, out hit))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                Debug.Log("tag is "  +hit.collider.tag + "slope angel is " 
                + slopeAngle + "hitdistance is: " + hit.distance) ;

                float distanceToSlope = hit.distance;
                gradientZ *= distanceToSlope;

                if (hit.collider.gameObject.tag == "Slope" && slopeAngle <= maxClimbAngle)
                {    
                    float gradientY = Mathf.Tan(slopeAngle* Mathf.Deg2Rad) * Mathf.Abs(gradientZ);
                   // direction is influenced by physics
                    Vector3 dir = new Vector3(0, gradientY, gradientZ); 
                    rb.MovePosition(control.transform.position + dir * Time.deltaTime);   
                } 
            }
        }
        */

    }
}
