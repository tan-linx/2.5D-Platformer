using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    public enum TransitionConditionType
    {
        MOVE,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,
        GRABBING_ROPE,
    }
    
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/TransitionIndexer")]
    public class TransitionIndexer:StateData
    {  
        [SerializeField]
        private int Index;
        [SerializeField]
        private List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>(); 
        
        private int transitionIndexerHash;
        private CharacterControl control;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            transitionIndexerHash = Animator.StringToHash("TransitionIndex");
            if (DoTransition()) 
            {
                animator.SetInteger(transitionIndexerHash, Index);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {    
            GetColliderTag();
            if (DoTransition())
            {
                animator.SetInteger(transitionIndexerHash, Index);
            }
            else
            {
                animator.SetInteger(transitionIndexerHash, 0);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(transitionIndexerHash, 0);
        }

        private bool DoTransition()
        {       
            foreach(TransitionConditionType c in transitionConditions)
            {
                switch (c)
                {
                    case TransitionConditionType.MOVE:
                    {
                        if (!control.MoveLeft && !control.MoveRight)
                        {
                            return false;
                        }
                    }
                    break;
                    case TransitionConditionType.UP:
                    {
                        if (!control.MoveUp)
                        {
                            return false;
                        }
                    }
                    break;
                    case TransitionConditionType.DOWN:
                    {
                        if (!control.Crouch)
                        {
                            return false;
                        }
                    }
                    break;
                    case TransitionConditionType.JUMP:
                    {
                        if (!control.Jump)
                        {
                            return false;
                        }
                    }
                    break;
                    case TransitionConditionType.GRABBING_ROPE:
                    { 
                      
                        if (!GetColliderTag().Equals("Rope"))
                        {
                            return false;
                        }
                    }
                    break;
                    case TransitionConditionType.GRABBING_LEDGE:
                    {
                        if (!control.LedgeChecker.IsGrabbingLedge)
                        {                          
                            return false;
                        }
                    }
                    break;
                }
            }
            return true;
        }


        private string GetColliderTag() 
        {
            RaycastHit hit;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 dir = Vector3.forward;
            if (control.MoveLeft) dir = Vector3.back;
            Debug.DrawRay(control.transform.position+Vector3.up*(collider.height/2), dir*collider.radius, Color.yellow);
            if (Physics.Raycast(control.transform.position+Vector3.up*(collider.height/2), dir, out hit, collider.radius)) 
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