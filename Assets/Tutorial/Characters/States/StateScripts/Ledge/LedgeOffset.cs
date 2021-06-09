using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Class <c>CheckFront</c> Moves the Player by a certain offset when
/// he collided with the ledge </summary>
namespace  Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/LedgeOffset")]
    public class LedgeOffset : StateData
    {        
        [SerializeField]
        private Vector3 alignmentOffset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //alignmentOffset = new Vector3(0, -0.76f, 0.58f);
            //Or for other anim: new Vector(0, -0.645, 0.26)
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.Animator.gameObject;
            anim.transform.localPosition = alignmentOffset; //control.LedgeChecker.LowerCollider.alignmentOffset;
            control.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}