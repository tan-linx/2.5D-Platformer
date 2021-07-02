using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Falling")]
    public class Falling:StateData
    {  
        private CharacterControl control;   
        private RaycastHit hitInfo;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            animator.SetBool(crashHash,false);

            if (IsFallToDeath(9)) 
            {
                Debug.Log("Fall to Death");
                control.dead = true;
                animator.SetBool("Dead", true);
            }
            else {
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

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {}

        private bool IsFallToDeath(float height) 
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();        
            Vector3 dir = Vector3.down*height;
            Vector3 rayOrigin = collider.bounds.center;
            Debug.DrawRay(rayOrigin, dir*height);
            if (Physics.Raycast(rayOrigin, dir, out hitInfo, height) && !IsRagdollPart(control, hitInfo.collider)) 
            {
                return false; 
            } 
            else {
                return true;   
            }  
        }   
    }
}