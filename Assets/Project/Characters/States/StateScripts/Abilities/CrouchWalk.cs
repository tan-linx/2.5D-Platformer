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
            if (!control.Crouch)
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
    }
}