using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        [SerializeField] 
        private AnimationCurve SpeedGraph;
        
        [SerializeField] 
        private float Speed;
        private CharacterControl control;
        private Rigidbody rb;
    
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(moveHash, false);
                return;
            }
            if (control.MoveRight && !CheckFront(control, Vector3.forward))
            {
                if (GetColliderTag(Vector3.forward) == "Rope" && control.currentHitCollider.attachedRigidbody.velocity.y < 3f) 
                {
                    animator.SetBool("Hanging", true);
                    return;
                }   
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.MovePosition(rb.position+(Vector3.forward*Speed*
                   SpeedGraph.Evaluate(stateInfo.normalizedTime) *Time.deltaTime));
            }
            if (control.MoveLeft && !CheckFront(control, Vector3.back))
            {
                if (GetColliderTag(Vector3.back) == "Rope" && control.currentHitCollider.attachedRigidbody.velocity.y < 3f) 
                {
                    animator.SetBool("Hanging", true);
                    return;
                }  
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.MovePosition(rb.position+(Vector3.back*Speed*
                    SpeedGraph.Evaluate(stateInfo.normalizedTime)*Time.deltaTime));
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        private string GetColliderTag(Vector3 dir) 
        {
            RaycastHit hit;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            if (Physics.SphereCast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y), 
            collider.bounds.extents.z, dir, out hit, collider.bounds.extents.z))
            { 
                if (!IsRagdollPart(control, hit.collider))
                {
                    control.currentHitCollider = hit.collider;
                    return hit.collider.gameObject.tag;
                }
            }
            return "";
        }
    }
}