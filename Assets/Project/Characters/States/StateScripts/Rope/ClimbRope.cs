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


            control.GetComponent<ConfigurableJoint>().connectedBody = null; 
            SetTriggerRopeColliders(control.transform.root, true);
            //control.gameObject.GetComponent<CapsuleCollider>().isTrigger= false;
            control.RIGID_BODY.isKinematic = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            capsuleScaleY = control.currentHitCollider.bounds.size.y; 
            float Speed = capsuleScaleY; 

            GameObject currentParentCapsule = control.transform.parent.gameObject; 
            if (control.MoveUp && currentParentCapsule.name != "RopeStart") 
            {    
                //TODO: Fix this movement here
                Vector3 dir = control.currentHitCollider.transform.parent.position-control.currentHitCollider.transform.position;
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
        }

        private void MoveUpCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.parent, false);
            Debug.Log(control.transform.parent);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();
            distanceMovedUp = 0f;
        }

        private void MoveDownCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.GetChild(0), false);
            Debug.Log(control.transform.parent);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();;
            distanceMovedUp = capsuleScaleY;
        }

        private void Climb()
        {
            Vector3 from = control.currentHitCollider.transform.position;
            Vector3 to = control.currentHitCollider.transform.parent.position;
            // Debug.Log(distance);
            Vector3 endPos = Vector3.Lerp(from, to, 0.3f);
            //distanceMovedUp+=distance;
        }        
    }
}