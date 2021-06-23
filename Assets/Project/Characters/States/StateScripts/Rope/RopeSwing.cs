using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/RopeSwing")]
    public class RopeSwing : StateData
    {   
        private CharacterControl control;
        private Rigidbody ropePartRB ;

        [SerializeField]
        private float Force;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            Force = 10f;
            animator.SetBool(jumpHash, false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ropePartRB = control.currentHitCollider.attachedRigidbody;
            if (control.MoveLeft)
            {
                ropePartRB.AddForce(Vector3.back*Force);
            }
            if (control.MoveRight)
            {
                ropePartRB.AddForce(Vector3.forward*Force);
            }
            if (control.Jump)
            {
                OnExitJump();
                animator.SetBool(jumpHash , true);
                return;
            }
            if (control.MoveUp || control.Crouch)
            {
                animator.SetBool(climbHash, true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        
        }

        private void OnExitJump()
        {

            if (control.gameObject.GetComponent<ConfigurableJoint>() != null)
            {
                control.gameObject.GetComponent<ConfigurableJoint>().connectedBody = null;
                Destroy(control.gameObject.GetComponent<ConfigurableJoint>());
            }    
            control.transform.parent = null;
            control.grabbingRope = false;
        }
    }
}