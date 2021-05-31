using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/CrouchWalk")]
    public class CrouchWalk: StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        private int crouchHash = Animator.StringToHash("Crouch"); 
        private int moveHash = Animator.StringToHash("Move"); 
        private float Speed = 1.8f;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //stop crouching here
            if (!control.Crouch)
            {
                animator.SetBool(crouchHash, false);
                return;
            }   
            if (!control.MoveLeft || !control.MoveRight)
            {
                animator.SetBool(moveHash, false);
                return;
            }
            //check whether one of this might be reusable from Running Class
            if (control.MoveRight)
            {                    
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CollisionChecker.CheckFront(control))
                {
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
                }
            } 
            if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CollisionChecker.CheckFront(control))
                {
                    rb.MovePosition(rb.position+(-Vector3.forward*Speed*Time.deltaTime));
                }     
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}