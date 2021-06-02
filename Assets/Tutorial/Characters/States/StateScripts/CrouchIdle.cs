using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/CrouchIdle")]
    public class CrouchIdle : StateData
    {
        private CharacterControl control;
        // TransitionParameters
        private int crouchHash = Animator.StringToHash("Crouch"); 
        private int moveHash = Animator.StringToHash("Move"); 

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //stop crouching here 
            //if head doesnt get bumped
            if (!control.Crouch && !CollisionChecker.CheckHead(control))
            {
                animator.SetBool(crouchHash, false);
                return;
            }   
            //to change state to moveCrouchState
            if (control.MoveLeft || control.MoveRight)
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