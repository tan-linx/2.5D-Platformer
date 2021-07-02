using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Dead")]
    public class Dead: StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {  
            characterState.GetCharacterControl(animator).dead = true;
        }
    }
}