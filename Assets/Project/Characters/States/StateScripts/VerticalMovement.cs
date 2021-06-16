using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/VerticalMovement")]
    public class VerticalMovement : StateData
    {
        private CharacterControl control;
        private Animator anim;
        private float Speed;
        float distanceMovedUpwards;
        float distanceMovedDownwards;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            anim = control.Animator;
            Speed = 0.5f;
            distanceMovedUpwards = 0f;
            distanceMovedDownwards = 0f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveUp) 
            {
                anim.transform.localPosition+=Vector3.down*Speed*Time.deltaTime;
                distanceMovedUpwards+=Speed*Time.deltaTime;
                distanceMovedDownwards-=Speed*Time.deltaTime;
            }
            if (control.Crouch) 
            {
                anim.transform.localPosition+=Vector3.up*Speed*Time.deltaTime;
                distanceMovedUpwards-=Speed*Time.deltaTime;
                distanceMovedDownwards+=Speed*Time.deltaTime;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void changeParentRopePart() {
            if (distanceMovedUpwards > anim.transform.parent.transform.localScale.y) //distance bigger than y-size of capsule
            {
                if (anim.transform.parent == anim.transform.root)  
                {
                    //do nothing        
                } else 
                {
                    anim.transform.parent = anim.transform.parent.gameObject.transform.parent.gameObject.transform;
                }
                distanceMovedDownwards = 0;
                distanceMovedUpwards = 0;
            }    
            if (distanceMovedDownwards > anim.transform.parent.transform.localScale.y) 
            {
                anim.transform.parent = anim.transform.parent.GetChild(0); //parent.GetComponent<HingeJoint>().connectedBody;
                distanceMovedDownwards = 0;
                distanceMovedUpwards = 0;
            }    
        }
    }
}