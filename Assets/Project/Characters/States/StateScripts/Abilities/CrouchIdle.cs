using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/CrouchIdle")]
    public class CrouchIdle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (!control.Crouch && !CheckHead(control))
            {
                animator.SetBool(crouchHash, false);
                return;
            }   
            if ((control.MoveLeft && !CrouchCheckFront(control, Vector3.back))||
                (control.MoveRight && !CrouchCheckFront(control, Vector3.forward)))
            {
                animator.SetBool(moveHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}
    }
}