using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Push")]
    
    /// <summary>Class <c>Push</c> Pushes an object forward.
    /// inspirations: https://github.com/DonHaul/2D-Tuts/blob/master/Assets/69%20-%20Pulland%20Pushblock/playerpush.cs
    /// https://github.com/DonHaul/2D-Tuts/blob/master/Assets/69%20-%20Pulland%20Pushblock/playerpush.cs
    ///</summary>
    public class Push : StateData
    {
        [SerializeField]
        private float Speed;

        private CharacterControl control;
        private Rigidbody rb;
        private GameObject pushable;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            StateGuard();
            Speed = 5f;
            rb = control.RIGID_BODY;
            animator.transform.localPosition = new Vector3(0f, -0.9914604f, -0.5f);
            pushable = control.currentHitCollider.gameObject;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {    
            if (control.MoveRight)
            {
                if (control.currentHitDirection.z != 1f)
                {
                    animator.SetBool(pushHash, false);
                    return;
                }
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.MovePosition(rb.position+(Vector3.forward*Speed*Time.deltaTime));
                pushable.transform.Translate(Vector3.forward*Speed*Time.deltaTime);
            }
            if (control.MoveLeft)
            {
                if (control.currentHitDirection.z != -1f)
                {
                    animator.SetBool(pushHash, false);
                    return;
                }
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.MovePosition(rb.position+(Vector3.back*Speed*Time.deltaTime));
                pushable.transform.Translate(Vector3.back*Speed*Time.deltaTime);
            }    
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(pushHash, false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(pushHash, false);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.transform.localPosition = new Vector3(0f, -0.9914604f, 0.04284224f);
        }
        
        private void StateGuard() 
        {
            if (control.currentHitCollider == null || control.currentHitCollider.tag != "Pushable") 
                throw new Exception("False State Error. Object not pushable");       
        }
    }
}