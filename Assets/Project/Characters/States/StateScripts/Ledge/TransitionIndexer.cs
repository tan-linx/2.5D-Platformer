using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://drive.google.com/drive/folders/1sQnid4-4EMlJgX_DZ6YUAxFbGeo6mXUE
namespace Platformer_Assignment
{
    public enum TransitionConditionType
    {
        MOVE,
        UP,
        GRABBING_LEDGE,
    }
    
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/TransitionIndexer")]
    public class TransitionIndexer:StateData
    {  
        [SerializeField] private int index;
        [SerializeField] private List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>(); 
        
        private int transitionIndexerHash;
        private CharacterControl control;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            transitionIndexerHash = Animator.StringToHash("TransitionIndex");
            if (DoTransition()) 
            {
                animator.SetInteger(transitionIndexerHash, index);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {    
            if (DoTransition())
            {
                animator.SetInteger(transitionIndexerHash, index);
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
    }
}