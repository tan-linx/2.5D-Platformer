using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Jump")]
    public class Jump : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;
        
        [SerializeField]
        private float JumpForce = 200f;
        [SerializeField]
        private AnimationCurve Gravity;
        [SerializeField]
        private AnimationCurve Pull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control =  characterState.GetCharacterControl(animator);
            Rigidbody rb = control.RIGID_BODY;
            control.RIGID_BODY.AddForce(Vector3.up * JumpForce);
            animator.SetBool(groundedHash, false); //TODO:
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
            if (GetColliderTag() == "Rope") 
            {
                animator.SetBool("Hanging", true);
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
           // animator.SetBool(jumpHash, false);
            //animator.SetBool(moveHash, false);
        }

        private string GetColliderTag() 
        {
            RaycastHit hit;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 dir = Vector3.forward;
            if (control.MoveLeft) dir = Vector3.back;
            Debug.DrawRay(control.transform.position+Vector3.up*(collider.height/2), dir*collider.radius, Color.yellow);
            //Gizmos.DrawSphere(collider.bounds.center+Vector3.up*(collider.bounds.extents.y/2), collider.bounds.extents.z); 
            if (Physics.SphereCast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y), 
            collider.bounds.extents.z, dir, out hit, collider.bounds.extents.z))
            //if(Physics.Raycast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y/2), dir, out hit, collider.bounds.extents.z)) 
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