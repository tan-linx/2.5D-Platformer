using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/ClimbLadder")]
    public class ClimbLadder : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        
        [SerializeField] private Vector3 animatorOffset;
        private float speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            speed = 1.5f;           
            rb.isKinematic = true;
            IsClimbDown();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveUp)
            {
                animator.speed = 1.5f;
                rb.MovePosition(rb.position + Vector3.up*speed*Time.deltaTime); 
                control.LedgeChecker.enabled = true;
            }
            //move down ladder
            if (control.Crouch && !IsLadderEnd())
            {
                animator.speed = 1.5f;
                rb.MovePosition(rb.position + Vector3.down*speed*Time.deltaTime);
                control.LedgeChecker.enabled = false;
            }
            if (!control.MoveUp && !control.Crouch && !control.MoveRight && !control.MoveLeft)   
            {
                control.LedgeChecker.enabled = true;
                animator.speed = 0f; 
            }
            //e.g. if he moves left while facing the ladder right he falls down
            if ((control.currentHitDirection == HitDirection.FORWARD && control.MoveLeft)
                || (control.currentHitDirection == HitDirection.BACK && control.MoveRight))
            {
                animator.speed = 1f;
                OnExitFallFromLadder();
                animator.SetBool(climbHash, false);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //reset modified variables
            control.LedgeChecker.enabled = true;
            control.RIGID_BODY.isKinematic = false;
            control.IsClimbDownLadder = false;
            control.currentHitDirection = HitDirection.None;
            control.currentHitCollider = null;
        }

        /// <summary>method <c>OnExitFallFromLadder</c> 
        /// Adds forward force to player when he falls from ladder </summary>
        private void OnExitFallFromLadder() 
        {
            control.RIGID_BODY.isKinematic = false;
            if (control.currentHitDirection == HitDirection.FORWARD)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.AddForce(Vector3.back*10f);
            }
            if (control.currentHitDirection == HitDirection.BACK)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.AddForce(Vector3.forward*10f);
            }
        }

        private bool IsLadderEnd() 
        {
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            return Physics.Raycast(control.GetComponent<CapsuleCollider>().bounds.center, 
                                    Vector3.down, 0.2f);
        }

        /// <summary>method <c>OnExitFallFromLadder</c> 
        /// Checks whether should climb down </summary>
        private void IsClimbDown()
        {            
            if (control.currentHitCollider)
            {
                if (control.IsClimbDownLadder && control.currentHitCollider.tag == "LadderDown")
                {
                    Vector3 teleportBox = control.currentHitCollider.transform.GetChild(0).position;
                    Debug.Log(teleportBox);
                    if (control.currentHitDirection == HitDirection.BACK) //&& control.currentHitCollider.gameObject.name == "LadderDownVolumeRight")
                        control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    if (control.currentHitDirection == HitDirection.FORWARD) // && control.currentHitCollider.gameObject.name == "LadderDownVolumeLeft")
                        control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    control.transform.position = teleportBox; 
                }
                control.IsClimbDownLadder = false; 
            }
        }
    }
}