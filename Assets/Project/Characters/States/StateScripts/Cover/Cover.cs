using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Cover")]
    public class Cover : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        private Vector3 initialPosition;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control =  characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            if (control.currentHitDirection == HitDirection.FORWARD)
            {
                initialPosition = animator.transform.localPosition;
                animator.transform.localPosition = new Vector3(0.14f, -0.978f, 0.47f);
                animator.transform.localEulerAngles = Vector3.up*86.162f;
            }
            if (control.currentHitDirection == HitDirection.BACK)
            {
                initialPosition = animator.transform.localPosition;
                animator.transform.localPosition = new Vector3(-0.14f, -0.978f, 0.47f);
                animator.transform.localEulerAngles = Vector3.up*-86.162f;
            }
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            float Speed = 1.5f;
            if (control.currentHitDirection == HitDirection.FORWARD && control.MoveRight)
            {                                
                animator.speed = 2f; 
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
            } 
            if (control.currentHitDirection == HitDirection.BACK && control.MoveLeft)
            {                
                animator.speed = 2f; 
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.MovePosition(rb.position+ (Vector3.back*Speed*Time.deltaTime));
            }
            if (!control.MoveRight && !control.MoveLeft)   
            {
                animator.speed = 0f; 
            }
            if (!CheckCover()) 
            {
                animator.SetBool("CoverLeft", false);
                animator.SetBool("CoverRight", false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            animator.speed = 1f; 
            control.currentHitCollider = null;
            control.currentHitDirection = HitDirection.None;
            animator.transform.localPosition = initialPosition;
            animator.transform.localEulerAngles = Vector3.zero;
        }

        /// <summary>method <c>CheckCover</c> Checks whether to leave the Cover animation. </summary>
        private bool CheckCover()
        {
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            Collider[] hitcolliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents);
            foreach(Collider hitcol in hitcolliders)
            {
                if (hitcol.tag == "Cover")
                    return true;
            }
            return false;
        }
    }
}