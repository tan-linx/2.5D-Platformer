using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/ClimbRope")]
    public class ClimbRope : StateData
    {
        private CharacterControl control;
        private float distanceMovedUp;
        private float Speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            Speed = 1f;
            distanceMovedUp = 0f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveUp) 
            {
                control.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
                SetTriggerRopeColliders(control.transform.root, true);
                control.RIGID_BODY.isKinematic = true;
                control.transform.localPosition+=Vector3.up*Speed*Time.deltaTime;
                distanceMovedUp+=Speed*Time.deltaTime;
                changeParentCapsule();
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void changeParentCapsule() {
            GameObject currentParentCapsule = control.transform.parent.gameObject;
            Debug.Log("localScale " + currentParentCapsule.transform.localScale.y + "Distance Moved up" + distanceMovedUp);
            if (distanceMovedUp >= currentParentCapsule.transform.localScale.y)
            {
                if (currentParentCapsule == control.transform.root)  
                {
                    return;      
                } 
                control.transform.SetParent(currentParentCapsule.transform.parent);
                control.currentHitCollider = control.transform.parent.gameObject.GetComponent<CapsuleCollider>();
                distanceMovedUp = 0f;
            }    
        }
    }
}