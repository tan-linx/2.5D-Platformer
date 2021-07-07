using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <author Tanja Schlanstedt></author>
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
            control.RIGID_BODY.isKinematic = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            capsuleScaleY = control.currentHitCollider.bounds.size.y; 
            float Speed = capsuleScaleY; 
            
            GameObject currentParentCapsule = control.transform.parent.gameObject; 
            if (control.MoveUp)  //TODO:
            {    
                if (distanceMovedUp < capsuleScaleY)
                {
                    Climb(Speed);
                }
                if (distanceMovedUp >= capsuleScaleY && currentParentCapsule.name != "RopeStart")
                {
                    MoveUpCapsule(currentParentCapsule);
                }
            }
            if (control.Crouch)
            {
                if (distanceMovedUp >= 0)
                {
                    Climb(-Speed);
                }
                if (distanceMovedUp < 0 && currentParentCapsule.transform.GetChild(0).tag == "Rope") 
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
            control.transform.SetParent(currentParentCapsule.transform.parent, true);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();
            distanceMovedUp = 0f;
        }

        private void MoveDownCapsule(GameObject currentParentCapsule) 
        {
            control.transform.SetParent(currentParentCapsule.transform.GetChild(0), true);
            control.currentHitCollider = control.transform.parent.GetComponent<CapsuleCollider>();;
            distanceMovedUp = capsuleScaleY;
        }

        /// <summary>method <c>Climb</c> Climb up the Rope and make the Player stick to it.</summary>        
        private void Climb(float speed)
        {
            float offsetToRope = 0.55f;
            if (control.currentHitDirection == HitDirection.FORWARD)
                offsetToRope *=-1;

            rb.MovePosition(new Vector3(control.currentHitCollider.transform.position.x, 
                                        rb.position.y+speed*Time.deltaTime, 
                                        control.currentHitCollider.transform.position.z+offsetToRope)); 
            distanceMovedUp+=speed*Time.deltaTime;
        }        
    }
}