using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/VerticalMovement")]
    public class ClimbRope : StateData
    {
        private CharacterControl control;
        private float distanceMovedUp;
        private GameObject currentParentCapsule;
        [SerializeField]
        private float Speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            Speed = 0.1f;
            distanceMovedUp = 0f;
            currentParentCapsule = control.currentHitCollider.gameObject;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveUp) 
            {
                control.transform.localPosition+=Vector3.up*Speed*Time.deltaTime;
                distanceMovedUp+=Speed*Time.deltaTime;
                changeParentCapsule();
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void changeParentCapsule() {
            if (distanceMovedUp > currentParentCapsule.transform.localScale.y)
            {
                if (control.transform.parent == control.transform.root)  
                {
                    //do nothing        
                } else 
                {
                    control.currentHitCollider = currentParentCapsule.transform.parent.gameObject.GetComponent<CapsuleCollider>();
                    control.transform.SetParent(currentParentCapsule.transform.parent);
                    distanceMovedUp = 0f;
                }
            }    
        }
    }
}