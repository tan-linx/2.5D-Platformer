using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Falling")]
    public class Falling:StateData
    {  
        private CharacterControl control;
        
        // to check whether it was a high fall
        private int crashHash; 

        private RaycastHit hit;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            crashHash = Animator.StringToHash("Crash");
        }

        //https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            
            if (IsCrash(15)) {
                float hitDistance = hit.distance;
                if (hitDistance >= 7) control.Dead = true;
                if (hitDistance < 7 && hitDistance > 4 )
                {
                    animator.SetBool(crashHash, true);
                }    
            }
            else 
            {
                animator.SetBool(crashHash, false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.Dead) control.TurnOnRagdoll();  
        }

        private bool IsCrash(float height) 
        {

            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();        
            Vector3 dir = Vector3.down;
            Vector3 rayOrigin = control.transform.position;
            Debug.DrawRay(rayOrigin, 
                dir*height, Color.green);                
            if (Physics.Raycast(rayOrigin, dir, out hit, height)) 
                {
                    return false; 
                } 
            else {
                return true;   
            }  
        }   
    }
}