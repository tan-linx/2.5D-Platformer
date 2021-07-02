using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Cover")]
    public class Cover : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control =  characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            if (control.currentHitDirection == Vector3.forward)
                animator.transform.localEulerAngles = Vector3.up*86.162f;
            else
                animator.transform.localEulerAngles = Vector3.up*-86.162f;
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            float Speed = 1.5f;
            if (control.currentHitDirection == Vector3.forward && control.MoveRight)
            {                                
                animator.speed = 2f; 
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
            } 
            if (control.currentHitDirection == Vector3.back && control.MoveLeft)
            {                
                animator.speed = 2f; 
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.MovePosition(rb.position+ (Vector3.back*Speed*Time.deltaTime));
            }
            if (!control.MoveRight && !control.MoveLeft)   
            {
                animator.speed = 0f;  //TODO:
            }
            if (!CheckCover()) 
            {
                animator.speed = 2f; 
                animator.SetBool("CoverLeft", false);
                animator.SetBool("CoverRight", false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control.currentHitCollider = null;
            control.currentHitDirection = Vector3.zero;
            animator.transform.localEulerAngles = Vector3.zero;
        }

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