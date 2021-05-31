using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Jump")]
    public class Jump : StateData
    {
        private float JumpForce = 100f;
        public AnimationCurve Gravity;
        public AnimationCurve Pull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Rigidbody rb = characterState.GetCharacterControl(animator).RIGID_BODY;
            // use root motion for normal jump in unity
            characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Rigidbody rb = characterState.GetCharacterControl(animator).RIGID_BODY;
            if (rb.velocity.y < 0f)
            {
               rb.velocity += (-Vector3.up * Gravity.Evaluate(stateInfo.normalizedTime));
            }

            if (rb.velocity.y > 0f && !control.Jump)
            {
                rb.velocity += (-Vector3.up * Pull.Evaluate(stateInfo.normalizedTime));
            }
            if (!control.Jump) {
                animator.SetBool(TransitionParameter.Jump.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
        }
    }
}