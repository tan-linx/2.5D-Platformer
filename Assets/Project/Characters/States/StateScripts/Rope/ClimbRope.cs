using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ClimbRope")]
    public class ClimbRope : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        private float capsuleScaleY;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            if (control.currentHitCollider.tag != "Rope") throw new Exception("CurrentCollider not Rope!");
            IsKinematicRope(control.transform.root, true);
            rb.isKinematic = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            capsuleScaleY = control.currentHitCollider.bounds.size.y; 
            float Speed = capsuleScaleY;
            Debug.Log(control.transform.parent.gameObject);
            GameObject currentParentCapsule = control.transform.parent.gameObject; 
            if (control.MoveUp && currentParentCapsule.name != "RopeStart") 
            {    
                
                rb.MovePosition(rb.position + Vector3.up*Speed*Time.deltaTime);
                distanceMovedUp+=Speed*Time.deltaTime;

                if (distanceMovedUp >= capsuleScaleY)
                {
                    MoveUpCapsule(currentParentCapsule);
                }
            }
            if (control.Crouch && currentParentCapsule.transform.GetChild(0).tag == "Rope")
            {
                rb.MovePosition(rb.position + Vector3.down*Speed*Time.deltaTime);
                distanceMovedUp-=Speed*Time.deltaTime;

                if (distanceMovedUp < 0) 
                {
                    MoveDownCapsule(currentParentCapsule);
                } 
            }
            if (!control.MoveUp && !control.Crouch) 
            {
                animator.SetBool(climbHash, false);
            }  
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //ERROR persists
            IsKinematicRope(control.transform.root, false);
            rb.isKinematic = false;
        }

        private void MoveUpCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.parent);
            Debug.Log(control.transform.parent);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();
            control.GetComponent<ConfigurableJoint>().connectedBody = control.currentHitCollider.attachedRigidbody;
            distanceMovedUp = 0f;
        }

        private void MoveDownCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.GetChild(0));
            Debug.Log(control.transform.parent);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();;
            control.GetComponent<ConfigurableJoint>().connectedBody = control.currentHitCollider.attachedRigidbody;
            distanceMovedUp = capsuleScaleY;
        }
    }
}