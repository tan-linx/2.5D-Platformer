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
            verticalRayCount = 3; //because of Running Jump
            if (!CheckFront(control)) 
            {
                if (control.MoveRight)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    rb.MovePosition(rb.position+(Vector3.forward*Speed*
                       SpeedGraph.Evaluate(stateInfo.normalizedTime) *Time.deltaTime));
                }

                if (control.MoveLeft)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    rb.MovePosition(rb.position+(Vector3.back*Speed*
                        SpeedGraph.Evaluate(stateInfo.normalizedTime) *Time.deltaTime));
                }
            }
            verticalRayCount = 5;
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}