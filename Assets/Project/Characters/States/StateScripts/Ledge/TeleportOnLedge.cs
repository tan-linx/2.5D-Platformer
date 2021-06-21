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
            endPosition = new Vector3(0, 0.43f, 0.36f);  
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.MoveRight) endPosition = Vector3.Scale(endPosition, new Vector3(1, 1, 1f));
            if (control.MoveLeft) endPosition =  Vector3.Scale(endPosition, new Vector3(1, 1, -1f));
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.Animator.gameObject;
            Vector3 endPosition = control.LedgeChecker.LowerCollider.transform.position 
            + this.endPosition; //  + control.LedgeChecker.LowerCollider.EndPosition;
            control.transform.position = endPosition;
            Vector3 localPosAnim = new Vector3(0f, -0.9914604f, 0.04284224f);
            anim.transform.localPosition = localPosAnim ;
        }
    }
}