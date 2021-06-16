using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/ToggleGravity")]
    public class ToggleGravity:StateData
    {  
        
        [SerializeField]
        public bool Enabled;
        [SerializeField]
        public bool OnStart;
        [SerializeField]
        public bool OnEnd;
        
        private CharacterControl control;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            if (OnStart)
            {
                ToggleGrav();
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
                
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                ToggleGrav();
            }
        }

        private void ToggleGrav() 
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.RIGID_BODY.useGravity = Enabled;
        }
    }
}