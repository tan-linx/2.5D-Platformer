using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Pull")]
    
    /// <summary>Class <c>Pull</c> To pull an Object. ///</summary>
    public class Pull : StateData
    {
        [SerializeField]
        private float Speed;

        private CharacterControl control;
        private Rigidbody rb;
        private GameObject pullable;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            StateGuard();
            Speed = 1.5f;
            rb = control.RIGID_BODY;
            pullable = control.currentHitCollider.gameObject;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {    
            if (control.currentHitDirection.z != 1f)
            {
                if (control.MoveLeft)
                {
                    animator.SetBool(pullHash, false);
                    return;
                }
                rb.MovePosition(rb.position+(Vector3.forward*Speed*Time.deltaTime));
                pullable.transform.Translate(Vector3.forward*Speed*Time.deltaTime);
            }
            if (control.currentHitDirection.z != -1f)
            {
                if (control.MoveRight)
                {
                    animator.SetBool(pullHash, false);
                    return;
                }
                rb.MovePosition(rb.position+(Vector3.back*Speed*Time.deltaTime));
                pullable.transform.Translate(Vector3.back*Speed*Time.deltaTime);   
            }
            if (!control.Pull)//&& !control.MoveLeft && !control.MoveRight) 
            {
                animator.SetBool(pullHash, false);
                return; 
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.currentHitDirection = Vector3.zero;
            control.currentHitCollider = null;
            Debug.Log("I left the pull state");
        }
        
        private void StateGuard() 
        {
            if (control.currentHitCollider == null || control.currentHitCollider.tag != "Pushable") 
                throw new Exception("False State Error. Object not pushable");       
        }
    }
}