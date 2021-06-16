using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Falling")]
    public class Falling:StateData
    {  
        private CharacterControl control;   
        private RaycastHit hitInfo;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            control.Dead = false;
        }

        //https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {       
            if (IsCrash(20)) 
            {
                control.Dead = true;
            }
            else {
                float hitDistance = hitInfo.distance;
                if (hitDistance > 7)
                {
                    animator.SetBool(crashHash, true);
                } 
                else {
                    animator.SetBool(crashHash,false);
                }   
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
           // if (control.Dead) control.TurnOnRagdoll();  
        }

        private bool IsCrash(float height) 
        {

            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();        
            Vector3 dir = Vector3.down*height;
            Vector3 rayOrigin = collider.bounds.center;
            Debug.DrawRay(rayOrigin, 
                dir, Color.green);                
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