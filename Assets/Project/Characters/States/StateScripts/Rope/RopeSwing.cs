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
            climbHash = Animator.StringToHash("Climb");
            control = characterState.GetCharacterControl(animator);
            Force = 20f;
            ropePartRB = control.currentHitCollider.gameObject.GetComponent<Rigidbody>(); 
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ropePartRB = control.currentHitCollider.gameObject.GetComponent<Rigidbody>(); 
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
            control.grabbingRope = false;
            if (control.gameObject.GetComponent<HingeJoint>() != null)
            {
                control.gameObject.GetComponent<HingeJoint>().connectedBody = null;
                Destroy(control.gameObject.GetComponent<HingeJoint>());
            }
            control.transform.parent = null;
            SetTriggerRopeColliders(ropePartRB.transform.root, true);    
            Rigidbody rb = control.RIGID_BODY;        
            rb.MovePosition(rb.position+Vector3.forward*ropePartRB.velocity.z);
        }
    }
}