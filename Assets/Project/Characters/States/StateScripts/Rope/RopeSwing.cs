using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    /// <summary>Class <c>RopeSwing</c> This class aplies Force to RopePart.</summary>
    /// for help: https://stackoverflow.com/questions/31740141/programmatically-attach-two-objects-with-a-hinge-joint#31741920
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/RopeSwing")]
    public class RopeSwing : StateData
    {
        private CharacterControl control;
        private Rigidbody ropePartRB ;

        [SerializeField]
        private float Force;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            Force = 20f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ropePartRB = control.currentHitCollider.gameObject.GetComponent<Rigidbody>(); 
            if (control.MoveLeft)
            {
                ropePartRB.AddForce(Vector3.back*Force);
            }
            if (control.MoveRight)
            {
                ropePartRB.AddForce(Vector3.forward*Force);
            }
            if (control.Jump)
            {
                animator.SetBool(jumpHash , true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control.grabbingRope = false;

            control.gameObject.GetComponent<HingeJoint>().connectedBody = null;
            Destroy(control.gameObject.GetComponent<HingeJoint>());
            control.transform.parent = null;
            
            SetTriggerRopeColliders(ropePartRB.transform.root, true);    
            Rigidbody rb = control.RIGID_BODY;        
            rb.MovePosition(rb.position+Vector3.forward*ropePartRB.velocity.z*10f);
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