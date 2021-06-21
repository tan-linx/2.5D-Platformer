using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ToggleCapsuleCollider")]
    public class ToggleCapsuleCollider : StateData
    {
        [SerializeField]
        private bool On;
        [SerializeField]
        private bool OnStart;
        [SerializeField]
        private bool OnEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
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
            if (OnEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleCapsuleCol(control);
            }
        }

        private void ToggleCapsuleCol(CharacterControl control)
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.GetComponent<CapsuleCollider>().enabled = On;
        }
    }
}