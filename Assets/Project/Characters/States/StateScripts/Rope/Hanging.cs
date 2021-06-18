using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Hanging")]
    public class Hanging : StateData
    {
        private CharacterControl control;
        
        [SerializeField]
        private Vector3 offset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            SetUpHingeJoint();
            control.transform.SetParent(control.currentHitCollider.transform);
            control.transform.Translate(offset);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveLeft || control.MoveRight)
            {
                animator.SetBool(moveHash, true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void SetUpHingeJoint() {
            HingeJoint hj =  control.gameObject.AddComponent<HingeJoint>();
            hj.connectedBody = control.currentHitCollider.attachedRigidbody;
            HingeJoint hitHj = control.currentHitCollider.gameObject.GetComponent<HingeJoint>();
            hj.anchor =  hitHj.anchor;
            hj.axis =  hitHj.axis;
            JointSpring hingeSpring = hj.spring;
            hingeSpring.damper = 50;
            hj.useSpring = hitHj.useSpring;
            JointLimits hjlimits = hj.limits;
            hjlimits.max = 10;
            hj.useLimits = hitHj.useLimits;
            hj.enableCollision = hitHj.enableCollision;
        }
    }
}