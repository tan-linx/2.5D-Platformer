using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{

    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/JumpFromRope")]
    public class JumpFromRope : StateData
    {
        private CharacterControl control;
        private Rigidbody rb;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Rigidbody ropePartRB = control.currentHitCollider.gameObject.GetComponent<Rigidbody>(); 
            rb.MovePosition(rb.position + Vector3.forward*ropePartRB.velocity.z*10f*Time.deltaTime);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Rigidbody ropePartRB = control.currentHitCollider.gameObject.GetComponent<Rigidbody>(); 
            SetTriggerRopeColliders(ropePartRB.transform.root, false);    
            control.currentHitCollider = null;        
        }

        private void SetTriggerRopeColliders(Transform parent, bool on)
        {
            foreach(Transform child in parent) 
            {
                child.gameObject.GetComponent<CapsuleCollider>().isTrigger = on;
                SetTriggerRopeColliders(child, on);
            }
        }
    }
}