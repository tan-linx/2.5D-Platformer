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
        private float Speed;
        private float capsuleScaleY;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            climbHash = Animator.StringToHash("Climb");
            
            SetTriggerRopeColliders(control.transform.root, true);
            control.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            IsKinematicRope(control.transform.root, true);
            control.RIGID_BODY.isKinematic = true;

            if (control.currentHitCollider.tag != "Rope") throw new Exception("CurrentCollider not Rope!");
            capsuleScaleY = control.currentHitCollider.bounds.size.y;    
            Speed = capsuleScaleY;
            Debug.Log(capsuleScaleY);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Rigidbody rb = control.RIGID_BODY;
            capsuleScaleY = control.currentHitCollider.bounds.size.y; 
            Speed = control.currentHitCollider.bounds.size.y;
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
            if (control.Crouch)
            {
                rb.MovePosition(rb.position + Vector3.down*Speed*Time.deltaTime);
                distanceMovedUp-=Speed*Time.deltaTime;

                if (distanceMovedUp < 0 && currentParentCapsule.transform.GetChild(0)!=null) 
                {
                    if (currentParentCapsule.transform.GetChild(0).tag != "Rope") throw new Exception("Child is not a rope");
                    if (currentParentCapsule.transform.GetChild(0).name == "RopeEnd")
                    {
                        animator.SetBool(climbHash, false);
                        return;
                    }
                    MoveDownCapsule(currentParentCapsule);
                } 
            }
            if (!control.MoveUp && !control.Crouch) 
            {
                Debug.Log(climbHash);
                animator.SetBool(climbHash, false);
            }  
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            SetTriggerRopeColliders(control.transform.root, false);    //TODO: maybe velocity rb check
            control.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            IsKinematicRope(control.transform.root, false);
            control.RIGID_BODY.isKinematic = false;
        }

        private void MoveUpCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.parent);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();
            control.GetComponent<HingeJoint>().connectedBody = control.currentHitCollider.attachedRigidbody;
            distanceMovedUp = 0f;
        }

        private void MoveDownCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.GetChild(0)); //if child == null jump
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();;
            control.GetComponent<HingeJoint>().connectedBody = control.currentHitCollider.attachedRigidbody;
            distanceMovedUp = capsuleScaleY;
        }
    }
}