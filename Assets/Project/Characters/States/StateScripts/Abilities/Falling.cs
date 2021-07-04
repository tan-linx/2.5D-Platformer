using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Falling")]
    public class Falling:StateData
    {  
        [SerializeField] private float delayTime;

        private CharacterControl control;   
        private RaycastHit hitInfo;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
            control = characterState.GetCharacterControl(animator);
            animator.SetBool(crashHash,false);
            ForceFall();
            if(stateInfo.normalizedTime>=delayTime)
            {
                if (IsFallToDeath(9)) 
                {
                    Debug.Log("Fall to Death");
                    animator.SetBool("Dead", true);
                }
                else {
                    Debug.Log("No to Death" + hitInfo.collider.tag );
                    float hitDistance = hitInfo.distance;
                    if (hitDistance > 7)
                    {
                        animator.SetBool(crashHash, true);
                    } 
                    else 
                    {
                        animator.SetBool(crashHash,false);
                    }   
                }
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {}

        private bool IsFallToDeath(float height) 
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();        
            Vector3 dir = Vector3.down*height;
            Vector3 rayOrigin = collider.bounds.center;
            if (Physics.Raycast(rayOrigin, dir, out hitInfo, height-control.distanceFallen)) 
            {
                return false; 
            } 
            else {
                return true;   
            }  
        }

        private void ForceFall()
        {
            Rigidbody rb = control.RIGID_BODY;
            if (control.MoveRight || control.MoveLeft)
            {
                control.RIGID_BODY.AddForce(Vector3.forward*rb.velocity.z*100f);
            }
        }   
    }
}