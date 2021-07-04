using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/RopeSwing")]
    public class RopeSwing : StateData
    {   
        private CharacterControl control;
        private Rigidbody ropePartRB ;

        [SerializeField] private float force;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            force = 10f;
            
            animator.SetBool(jumpHash, false);
            SetTriggerRopeColliders(control.transform.root, false);
            IsKinematicRope(control.transform.root, false);

            control.GetComponent<ConfigurableJoint>().connectedBody = control.currentHitCollider.attachedRigidbody; 
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ropePartRB = control.currentHitCollider.attachedRigidbody;
            if (control.MoveLeft)
            {
                ropePartRB.AddForce(Vector3.back*force);
            }
            if (control.MoveRight)
            {
                ropePartRB.AddForce(Vector3.forward*force);
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
                Destroy(control.gameObject.GetComponent<ConfigurableJoint>());
            }    
            control.transform.parent = null;
            control.IsGrabbingRope = false;
        }
    }
}