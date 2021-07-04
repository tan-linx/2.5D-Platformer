using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Jump")]
    public class Jump : StateData
    {
        private CharacterControl control;        
        [SerializeField]
        private float jumpForce = 250f;
        [SerializeField]
        private AnimationCurve gravity;
        [SerializeField]
        private AnimationCurve pull;

        private float firstTime;
        private float posYLastFrame;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control =  characterState.GetCharacterControl(animator);
            control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
            animator.SetBool(groundedHash, false); 
            firstTime = Time.time;
            control.distanceFallen = 0f;
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.GravityMultiplier = gravity.Evaluate(stateInfo.normalizedTime);
            control.PullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (control.RIGID_BODY.velocity.y < 0f)
            {
                float timeElapsed = Time.time - firstTime;
                control.distanceFallen += Mathf.Abs(control.transform.position.y - posYLastFrame);
                if (timeElapsed > 1.2f)
                {
                    if (DoFallTransition())
                    {
                        animator.SetBool(fallingHash, true);
                        return;
                    } else {
                        animator.SetBool(fallingHash, false);
                    }  
                }
                posYLastFrame = control.transform.position.y; 
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
        }

        private bool DoFallTransition()
        {   
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();  
            RaycastHit hit;      
            Vector3 dir = Vector3.down;
            float maxRayLength = 3f;
            Vector3 rayOrigin = collider.bounds.center;
            if (Physics.Raycast(rayOrigin, dir*maxRayLength, out hit, maxRayLength))
                return false; 
            else 
                return true;   
        }
    }
}