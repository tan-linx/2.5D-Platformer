using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {
        [SerializeField]
        private Vector3 endPosition; 
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            endPosition = new Vector3(0, 0.43f, 0.39f*control.currentHitDirection.z);  
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.Animator.gameObject;
            control.transform.position =control.LedgeChecker.LowerCollider.transform.position + this.endPosition;
            Vector3 localPosAnim = new Vector3(0f, -0.9914604f, 0.04284224f);
            anim.transform.localPosition = localPosAnim ;
        }
    }
}