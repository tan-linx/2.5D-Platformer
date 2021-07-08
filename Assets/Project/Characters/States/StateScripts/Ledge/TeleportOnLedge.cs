using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://drive.google.com/drive/folders/1-maPp6rXbc6NkPbjf61ygwq4sqdX8c3n
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {
        private Vector3 endPosition; 
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.currentHitDirection == HitDirection.FORWARD)
                endPosition = new Vector3(0, 0.44f, 0.39f);  
            else 
                endPosition = new Vector3(0, 0.44f, -0.39f);  
    
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){ }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.transform.position = control.LedgeChecker.LowerCollider.transform.position + endPosition;
            animator.transform.localPosition = new Vector3(0f, -0.9914604f, 0.04284224f);
        }
    }
}