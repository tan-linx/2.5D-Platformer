using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://drive.google.com/drive/folders/1URSCwsxxxJsWg9ctpTEWFCbM3KhOH4mO
// https://www.youtube.com/watch?v=tjV7E9WITKQ&list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6&index=12
namespace Platformer_Assignment {
    public class CharacterState : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>();
        private CharacterControl control;
        
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

        /// <summary>method <c>GetCharacterControl</c> Returns the CharacterControl assigned to the animator</summary> ;
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
