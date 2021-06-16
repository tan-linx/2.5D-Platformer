using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/PushForward")]
    
    /// <summary>Class <c>PushForward</c> Pushes an object forward.
    ///</summary>
    public class PushForward : StateData
    {
        private float Speed;

        bool lastMoveLeft;
        bool lastMoveRight;

        private CharacterControl control;
        private Rigidbody rb;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            Speed = 2f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }

            if (control.MoveRight 
                && lastMoveRight == control.MoveRight
                && control.currentHitCollider.transform.position.z > control.transform.position.z)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Vector3 stepSize = Vector3.forward*Speed*Time.deltaTime;
                rb.MovePosition(rb.position+stepSize);
                control.currentHitCollider.transform.Translate(stepSize);
            }

            if (control.MoveLeft && lastMoveLeft == control.MoveLeft
              && control.currentHitCollider.transform.position.z < control.transform.position.z)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                Vector3 stepSize = Vector3.back*Speed*Time.deltaTime;
                rb.MovePosition(rb.position+stepSize);
                control.currentHitCollider.transform.Translate(stepSize);
            } 
            lastMoveLeft = control.MoveLeft;
            lastMoveRight = control.MoveRight;
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}