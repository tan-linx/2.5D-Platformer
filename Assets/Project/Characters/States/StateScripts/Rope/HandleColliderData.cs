using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    public enum TransitionColliders
    {
        CROUCH,
        PUSH,
        GRABBING_ROPE,
    }
    
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/HandleColliderData")]
    public class HandleColliderData:StateData
    {  
        [SerializeField]
        private int Index;
        
        [SerializeField]
        private List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>(); 
        
        private CharacterControl control;
        private List<string> tags = new List<string>();
        private int transitionIndexerHash;
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
            GetColliderTags();
            foreach(TransitionColliders c in transitionConditions)
            {
                switch (c)
                {
                    case TransitionColliders.GRABBING_ROPE:
                    {
                        if (!tags.Contains("Rope"))
                        {
                            return false;
                        }
                    }
                    break;
                }
            }
            return true;
        }

        private void GetColliderTags() 
        {
              
            float offset = 0f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Collider[] hitColliders = Physics.OverlapBox(
                control.transform.position, 
                new Vector3(collider.radius, collider.height+offset, collider.radius+offset),  
                control.transform.rotation); 
            foreach(Collider hitCollider in hitColliders) 
            {
                tags.Add(hitCollider.gameObject.tag);
            }   
        }
    }
}