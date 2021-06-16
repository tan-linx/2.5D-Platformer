using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Idle")]
    public class Idle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(crashHash, false);
            animator.SetBool(jumpHash, false);
            animator.SetBool(moveHash, false);
            animator.SetBool(crouchHash, false);
            animator.SetBool(pushHash, false);    
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.Jump)
            {
                animator.SetBool(jumpHash, true);
                return;
            } 
            if (control.Crouch)
            {
                animator.SetBool(crouchHash, true);
                return;
            }
            if (control.MoveRight)
            {
                animator.SetBool(moveHash, true);
                return; 
            } 
            if (control.MoveLeft)
            {
                animator.SetBool(moveHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}