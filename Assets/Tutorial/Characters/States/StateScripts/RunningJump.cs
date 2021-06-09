using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/RunningJump")]
    public class RunningJump : StateData
    {   
        private CharacterControl control;
        private Rigidbody rb;

        private float Speed;
        private float JumpForce;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            Speed = 10f;
            JumpForce = 2f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {            
            Vector3 dir;
            if (control.MoveLeft) dir = Vector3.back;
            else dir = Vector3.forward;
            if (!CheckFront(control)) {
                rb.MovePosition(control.transform.position
                +dir*Speed*Time.deltaTime+Vector3.up*JumpForce*Time.deltaTime);        
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

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
