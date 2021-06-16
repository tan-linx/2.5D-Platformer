using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    /**this class follows the purpose of updating the CharacterStates**/
    public class CharacterState : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData d in ListAbilityData) 
            {
                d.OnEnter(this, animator, stateInfo);
            }
        }

        public void UpdateAll(CharacterState characterState, 
        Animator animator, AnimatorStateInfo stateInfo) 
        {
            foreach(StateData d in ListAbilityData) 
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator,stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData d in ListAbilityData) 
            {
                d.OnExit(this, animator, stateInfo);
            }
        }

        /// <summary>method <c>GetCharacterControl</c> Returns the CharacterControl from the animator</summary> 
        private CharacterControl control;
        public CharacterControl GetCharacterControl(Animator animator)
         {
            if (control == null)
            {
                control = animator.GetComponentInParent<CharacterControl>();
            }
            return control;
        }
    }

}