using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* based on https://drive.google.com/drive/folders/1GXMtsJILOInq7kK86VuJf2NRt_SxDr8k
 modified:
    - added DoFallTransition(), CheckFall()
 */
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
            DoFallTransition(animator);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
        }

        private void DoFallTransition(Animator animator)
        {
            if (control.RIGID_BODY.velocity.y < 0f)
            {
                float timeElapsed = Time.time - firstTime;
                control.distanceFallen += Mathf.Abs(control.transform.position.y - posYLastFrame);
                if (timeElapsed > 1.3f)
                {
                    if (CheckFall())
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

        /// <summary> Checks whether transition to fall is needed </summary>
        private bool CheckFall()
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