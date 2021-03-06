using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <author Tanja Schlanstedt></author>
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
            Rigidbody ropePartRB = control.currentHitCollider.attachedRigidbody;
            rb.AddForce(Vector3.forward*70f);
            control.currentHitCollider = null;    
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {    
        }
    }
}