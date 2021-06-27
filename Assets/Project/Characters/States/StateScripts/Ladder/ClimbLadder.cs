using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ClimbLadder")]
    public class ClimbLadder : StateData
    {
        
        private CharacterControl control;
        private Rigidbody rb;
        
        [SerializeField]
        private Vector3 animatorOffset;
        private float speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;

            control.GetComponent<CapsuleCollider>().isTrigger = true;
            rb.isKinematic = true;

            speed = 1.5f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveUp)
            {
                animator.speed = 1.5f;
                rb.MovePosition(rb.position + Vector3.up*speed*Time.deltaTime);
            }
            if (control.Crouch)
            {
                animator.speed = 1.5f;
                rb.MovePosition(rb.position + Vector3.down*speed*Time.deltaTime);
            }
            //Climb Ladder Idle
            if (!control.MoveUp && !control.Crouch && !control.MoveRight && !control.MoveLeft)   
            {
                animator.speed = 0f; 
            }
            if ((control.currentHitDirection == Vector3.forward && control.MoveLeft))
            {
                animator.speed = 1.5f;
                OnExitFallFromLadder();
                animator.SetBool(climbHash, false);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.GetComponent<CapsuleCollider>().isTrigger = false;
            control.RIGID_BODY.isKinematic = false;
            //todo: might need to fix 
            animator.SetBool(climbHash, false); 
        }

        private void OnExitFallFromLadder() 
        {
            control.GetComponent<CapsuleCollider>().isTrigger = false;
            control.RIGID_BODY.isKinematic = false;
            if (control.currentHitDirection == Vector3.forward)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.AddForce(Vector3.back*10f);
            }
            if (control.currentHitDirection == Vector3.back)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.AddForce(Vector3.forward*10f);
            }
        }
    }
}