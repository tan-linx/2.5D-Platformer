using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/RunningJump")]
    public class RunningJump : StateData
    {   
        /**
        Split the jump
        1. do a Jump Prep
        2. do a Jump Middle
        3. Jump Fall

        Within the jump middle we will apply forces
        jump middle will loop like in the running animation
        **/

        private float timePassed;
        private float Speed = 5f;
        private float JumpForce = 1f;

        private CharacterControl control;
        private Rigidbody rb;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //timePassed = 0; 
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //timePassed += Time.deltaTime;
            if (!CollisionChecker.IsGrounded(control))  
            {
                animator.SetBool("Jump", false);
            }
            if (stateInfo.normalizedTime >= GetAnimationClipTime(animator, "RunningJump"))
            {
                animator.SetBool("Jump", false);
            }
            //rb.AddForce(Vector3.up * 100f)+velocity Doesnt work if body is kinematic
            //set Direction
            
            Vector3 dir = new Vector3(0, 0, 0); 
            if (control.MoveRight) dir = Vector3.forward;
            if (control.MoveLeft) dir = Vector3.back;
            if (!CollisionChecker.CheckFront(control)) {
                rb.MovePosition(control.transform.position
                +dir*Speed*Time.deltaTime);        
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        /**
        TODO: Move this somewhere else 
        **/ 
        public float GetAnimationClipTime(Animator animator, string wantedClipName) {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach(AnimationClip clip in clips)
            {
                if (clip.name==wantedClipName)
                        return clip.length;
            }
            return 0f;
        }
    }
}
