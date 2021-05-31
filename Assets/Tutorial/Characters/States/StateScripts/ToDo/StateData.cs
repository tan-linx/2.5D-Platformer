using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    public abstract class StateData : ScriptableObject {
        public abstract void OnEnter(CharacterState characaterState, 
        Animator animator, AnimatorStateInfo stateInfo); 

        /**
            condition is needed here to get out of the state !
        **/
        public abstract void UpdateAbility(CharacterState characaterState, 
        Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnExit(CharacterState characaterState, 
        Animator animator, AnimatorStateInfo stateInfo);
    }
}

