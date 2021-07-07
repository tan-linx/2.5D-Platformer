using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on ehttps://drive.google.com/drive/folders/15KfeQpdzglnQOwYz9xJA95J73NArboiN
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ForceTransition")]
    public class ForceTransition : StateData
    {
        
        [Range(0.01f, 1f)][SerializeField] private float transitionTiming = 1f;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >=transitionTiming) 
            {
                animator.SetBool(transitionHash, true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(transitionHash, false);
        }
    }
}