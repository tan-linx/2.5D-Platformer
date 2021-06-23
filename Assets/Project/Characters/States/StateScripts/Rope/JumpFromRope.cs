using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer_Assignment
{

    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/JumpFromRope")]
    public class JumpFromRope : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
            if (control.currentHitCollider.tag != "Rope") throw new Exception("Current Collider is not a Rope Part. You shouldn't be in this state"); 
            Rigidbody ropePartRB = control.currentHitCollider.attachedRigidbody;
            rb.MovePosition(rb.position + Vector3.forward*ropePartRB.velocity.z*3f*Time.deltaTime);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Rigidbody ropePartRB = control.currentHitCollider.attachedRigidbody;  
            control.currentHitCollider = null;        
        }
    }
}