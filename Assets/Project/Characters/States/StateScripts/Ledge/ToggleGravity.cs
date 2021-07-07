using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://drive.google.com/drive/folders/1M-ALI0HH4que2ZZ7ewFcsm35i7hzEu6y
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ToggleGravity")]
    public class ToggleGravity:StateData
    {  
        
        [SerializeField] public bool enabled;
        [SerializeField] public bool onStart;
        [SerializeField] public bool onEnd;
        
        private CharacterControl control;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            if (onStart)
            {
                ToggleGrav();
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                ToggleGrav();
            }
        }

        private void ToggleGrav() 
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.RIGID_BODY.useGravity = enabled;
        }
    }
}