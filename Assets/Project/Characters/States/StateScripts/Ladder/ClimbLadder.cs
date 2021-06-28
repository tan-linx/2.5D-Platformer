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
            IsClimbDown();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveUp)
            {
                animator.speed = 1.5f;
                rb.MovePosition(rb.position + Vector3.up*speed*Time.deltaTime);
                //change this 
                control.LedgeChecker.enabled = true;
            }
            if (control.Crouch && !IsLadderEnd())
            {
                animator.speed = 1.5f;
                rb.MovePosition(rb.position + Vector3.down*speed*Time.deltaTime);
                //disable Ledge Checker when climbing down
                control.LedgeChecker.enabled = false;
            }
            if (!control.MoveUp && !control.Crouch && !control.MoveRight && !control.MoveLeft)   
            {
                animator.speed = 0f; 
            }
            if ((control.currentHitDirection == Vector3.forward && control.MoveLeft)
                || (control.currentHitDirection == Vector3.back && control.MoveRight))
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
            control.climbDownLadder = false;
            control.currentHitCollider = null;
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

        private bool IsLadderEnd() 
        {
            Debug.Log("Ladder is end");
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            return Physics.Raycast(control.GetComponent<CapsuleCollider>().bounds.center, 
                                    Vector3.down, col.bounds.extents.y);
        }

        //TODO:
        private void IsClimbDown()
        {            
            ClimbDownGuard();
            if (control.climbDownLadder && control.currentHitCollider.tag == "LadderDown")
            {
                if (control.currentHitDirection == Vector3.back)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    control.transform.position = control.currentHitCollider.transform.GetChild(0).position; 
                }
                if (control.currentHitDirection == Vector3.forward)
                {  
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    control.transform.position = control.currentHitCollider.transform.GetChild(0).position;
                }
            }
            control.climbDownLadder = false; 
        }

        private void ClimbDownGuard() 
        {
            if (control.currentHitCollider == null || control.currentHitDirection == null)
                Debug.Log("There is something seriously wrong here. Fix this.");
        }
    }
}