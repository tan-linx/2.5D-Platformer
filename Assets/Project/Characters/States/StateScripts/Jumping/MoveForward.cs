using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=6Zg2Hgwg7OM&list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6&index=16
// https://drive.google.com/drive/folders/1v1Wkjsb8P8rQCnnCP0B0BBCsGHyWGx0o
// based on this tutorial 
// modified:  
// - moving with MovePostition instead of translate for smooth movement
// - added additional conditions to transition to rope swing and climbing steps 
namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        [SerializeField] 
        private AnimationCurve speedGraph;
        
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
            if (control.MoveRight)
            {
                if (!CheckFront(control, Vector3.forward)) 
                {
                    if (IsRopeCollider(control, Vector3.forward) && control.currentHitCollider.attachedRigidbody.velocity.y < 3f) 
                    {
                        animator.SetBool("Hanging", true);
                        return;
                    }   
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    rb.MovePosition(rb.position+(Vector3.forward*Speed*
                       speedGraph.Evaluate(stateInfo.normalizedTime)*Time.deltaTime));
                }   
            }
            if (control.MoveLeft)
            {
                if (!CheckFront(control, Vector3.back))
                {
                    if (IsRopeCollider(control, Vector3.back) && control.currentHitCollider.attachedRigidbody.velocity.y < 3f) 
                    {
                        animator.SetBool("Hanging", true);
                        return;
                    }  
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    rb.MovePosition(rb.position+(Vector3.back*Speed*
                        speedGraph.Evaluate(stateInfo.normalizedTime)*Time.deltaTime));
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}