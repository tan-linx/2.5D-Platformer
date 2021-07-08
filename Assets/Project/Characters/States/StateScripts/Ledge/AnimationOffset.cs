using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://drive.google.com/drive/folders/1TNpSikrwRICMdULGEqwLxHO5o2wyQ6Cw
// https://www.youtube.com/watch?v=z8gLOWpLYak&list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6&index=53
// modified the structure
/// <summary>Class <c>AnimationOffset</c> Moves the player by a certain offset when
/// he collided with the ledge </summary>
namespace  Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/AnimationOffset")]
    public class AnimationOffset : StateData
    {        
        [SerializeField] private Vector3 alignmentOffset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.Animator.gameObject;
            anim.transform.localPosition = alignmentOffset;
            control.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {}

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo){}
    }
}