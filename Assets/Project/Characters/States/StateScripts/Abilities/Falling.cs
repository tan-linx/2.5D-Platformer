using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
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
            if (IsFallToDeath(14
)) 
            {
                animator.SetBool("Dead", true);
            }
            else 
            {
                float hitDistance = hitInfo.distance;
                if (hitDistance > 5)
                {
                    animator.SetBool(crashHash, true);
                } 
                else 
                {
                    animator.SetBool(crashHash,false);
                }  
            }
            control.distanceFallen = 0f;  
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) 
        {
        }

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
    }
}