using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    this class checks whether Character is Grounded 
**/ 
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {
            CharacterControl control = characterState.GetCharacterControl(animator);
            {
                if (IsGrounded(control))
                {
                    control.Grounded = true;
                    //animator.SetBool(TransitionParameter.Grounded.ToString(), true);
                }
                else
                {
                    control.Grounded = false; 
                    //animator.SetBool(TransitionParameter.Grounded.ToString(), false);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        RaycastHit m_Hit;

        /// <summary>method <c>IsGrounded</c> Checks if Object is grounded</summary>
        bool IsGrounded(CharacterControl control)
        {
            //when velocity around 0 
            if (control.RIGID_BODY.velocity.y > -0.001f 
                && control.RIGID_BODY.velocity.y <= 0f)
            {
                return true;
            }     
            //when rigid_body is falling
            else if (control.RIGID_BODY.velocity.y <= 0f){

                CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
                float offset = .1f;
                return Physics.Raycast(control.transform.position, 
                Vector3.down*(collider.height/2 +offset));
            }
            return false;
        }
    }
}