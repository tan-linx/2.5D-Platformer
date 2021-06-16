using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    /// <summary>Class <c>RopeSwing</c> This class aplies Force to RopePart.
    ///</summary>
    /// for help: https://stackoverflow.com/questions/31740141/programmatically-attach-two-objects-with-a-hinge-joint#31741920
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/RopeSwing")]
    public class RopeSwing : StateData
    {
        [SerializeField]
        private GameObject TargetLeftLeg;

        [SerializeField]
        private GameObject TargetRightLeg;

        private CharacterControl control;
        private Rigidbody ropePartRB ;
        private Animator anim;
        private float Force;

      //  CapsuleCollider col;
        HingeJoint hinge ;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {   
            control = characterState.GetCharacterControl(animator);
            anim = control.Animator;
            Force = 10f;
            Debug.Log(control.currentHitCollider);

            anim.transform.parent = control.currentHitCollider.transform;
            //control.transform.parent = control.currentHitCollider.transform;
            Rigidbody animrb = anim.gameObject.AddComponent<Rigidbody>();
            animrb.useGravity = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ropePartRB = control.currentHitCollider.attachedRigidbody;
            anim.transform.parent = control.currentHitCollider.transform;
            anim.transform.localPosition = new Vector3(0, -16, 0);
            if (control.MoveLeft)
            {
                ropePartRB.AddForce(Vector3.back*Force);
                //TargetLeftLeg.transform.Translate(Vector3.forward * Time.deltaTime);
                //TargetRightLeg.transform.Translate(Vector3.forward*20f * Time.deltaTime);
               // Debug.Log("taretLeftLegPosition" + TargetLeftLeg.transform.position);
            }
            if (control.MoveRight)
            {
                ropePartRB.AddForce(Vector3.forward*Force);
                //TargetLeftLeg.transform.Translate(Vector3.back * 20f * Time.deltaTime);
                //TargetRightLeg.transform.Translate(Vector3.back * Time.deltaTime);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}