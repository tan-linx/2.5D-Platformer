using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Push")]
    
    /// <summary>Class <c>Push</c> Pushes an object forward. ///</summary>
    public class Push : StateData
    {
        [SerializeField] private float speed;

        private CharacterControl control;
        private Rigidbody rb;
        private GameObject pushable;
        private Vector3 transformBeforeTeleport;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            StateGuard();
            speed = 1.5f;
            rb = control.RIGID_BODY;
            transformBeforeTeleport = animator.transform.localPosition;
            animator.transform.localPosition = new Vector3(0f, -0.9914604f, -0.2f);
            pushable = control.currentHitCollider.gameObject;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {    
            if (control.MoveRight)
            {
                if (control.currentHitDirection == HitDirection.BACK)
                {
                    animator.SetBool(pushHash, false);
                    return;
                }
                if (IsBoxPushable()) 
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    rb.MovePosition(rb.position+(Vector3.forward*speed*Time.deltaTime));
                    pushable.transform.Translate(Vector3.forward*speed*Time.deltaTime);
                }
            }
            if (control.MoveLeft)
            {
                if (control.currentHitDirection == HitDirection.FORWARD)
                {
                    animator.SetBool(pushHash, false);
                    return;
                }
                if (IsBoxPushable()) 
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    rb.MovePosition(rb.position+(Vector3.back*speed*Time.deltaTime));
                    pushable.transform.Translate(Vector3.back*speed*Time.deltaTime);
                }
            }    
            if (!control.Push)
            {
                animator.SetBool(pushHash, false);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.currentHitDirection = HitDirection.None;
            control.currentHitCollider = null;
            animator.transform.localPosition = transformBeforeTeleport;//new Vector3(0f, -0.9914604f, 0.04284224f);
        }
        
        private bool IsBoxPushable()
        {
            return pushable.GetComponent<Pushable>().IsPushable;
        }

        private void StateGuard() 
        {
            if (control.currentHitCollider == null || control.currentHitCollider.tag != "Pushable") 
                throw new Exception("False State Error. Object not pushable");       
        }
    }
}