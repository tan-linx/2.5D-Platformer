using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://drive.google.com/drive/folders/1M-ALI0HH4que2ZZ7ewFcsm35i7hzEu6y
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ToggleCapsuleCollider")]
    public class ToggleCapsuleCollider : StateData
    {
        [SerializeField] private bool on;
        [SerializeField] private bool onStart;
        [SerializeField] private bool onEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleCapsuleCol(control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleCapsuleCol(control);
            }
        }

        private void ToggleCapsuleCol(CharacterControl control)
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.GetComponent<CapsuleCollider>().enabled = on;
        }
    }
}