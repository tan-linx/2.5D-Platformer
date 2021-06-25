using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Jump")]
    public class Jump : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        
        [SerializeField]
        private float JumpForce = 200f;
        [SerializeField]
        private AnimationCurve Gravity;
        [SerializeField]
        private AnimationCurve Pull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control =  characterState.GetCharacterControl(animator);
            Rigidbody rb = control.RIGID_BODY;
            control.RIGID_BODY.AddForce(Vector3.up * JumpForce);
            animator.SetBool(groundedHash, false); 
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
    
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
        }
    }
}