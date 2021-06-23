using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    /// <summary>Class <c>HangingTransitioner</c> This class prepares the Player for the Hanging on Rope.</summary>
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/RopeSwingTransitioner")]
    public class RopeSwingTransitioner : StateData
    {
        private CharacterControl control;
        
        [SerializeField]
        private Vector3 offset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {          
            control = characterState.GetCharacterControl(animator);
            control.grabbingRope = true;
            SetUpJoint();
            control.transform.Translate(offset);
            control.transform.SetParent(control.currentHitCollider.transform);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        private void SetUpJoint() {
            ConfigurableJoint cj =  control.gameObject.AddComponent<ConfigurableJoint>();
            cj.xMotion = ConfigurableJointMotion.Locked;
            cj.yMotion = ConfigurableJointMotion.Locked;
            cj.zMotion = ConfigurableJointMotion.Locked;
            ConfigurableJoint hitCj = control.currentHitCollider.gameObject.GetComponent<ConfigurableJoint>();
            cj.anchor =  hitCj.anchor;
            cj.axis =  hitCj.axis;
            cj.xMotion = hitCj.xMotion;
            cj.yMotion = hitCj.yMotion;
            cj.zMotion = hitCj.zMotion;
            cj.secondaryAxis = hitCj.secondaryAxis;
            cj.angularXMotion = hitCj.angularXMotion;
            cj.angularYMotion = hitCj.angularYMotion;
            cj.angularZMotion = hitCj.angularZMotion;
            cj.connectedBody = control.currentHitCollider.attachedRigidbody;
        }
    }
}