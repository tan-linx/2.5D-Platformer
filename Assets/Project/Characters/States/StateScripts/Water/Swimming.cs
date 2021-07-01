using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Platformer_Assignment
{
    [CreateAssetMenu(fileName = "New State", menuName = "Platformer/AbilityData/Swimming")]
    public class Swimming : StateData
    {        
        private CharacterControl control;
        private Rigidbody rb;
        private float Speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            control = characterState.GetCharacterControl(animator);
            rb = control.RIGID_BODY;
            rb.useGravity = false;
            Speed = 1.3f;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //add downwards force
            //rb.AddForce(Physics.gravity.normalized*0.3f);
            if (control.MoveRight) 
            {
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!CheckFront(control, Vector3.forward)) 
                {
                    rb.MovePosition(control.transform.position+Vector3.forward*Speed*Time.deltaTime);
                }
            }
            if (control.MoveLeft)
            {
                rb.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CheckFront(control, Vector3.forward)) 
                {
                    rb.MovePosition(control.transform.position+Vector3.back*Speed*Time.deltaTime);
                }
            }
            if (control.MoveUp)
            {
                rb.MovePosition(control.transform.position+Vector3.up*Speed*Time.deltaTime);
            }
            if (control.Crouch)
            {
                rb.MovePosition(control.transform.position+Vector3.down*Speed*Time.deltaTime);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private bool OnWaterSurface() 
        {
            CapsuleCollider col = control.GetComponent<CapsuleCollider>();
            RaycastHit hit;
            if (Physics.Raycast(col.bounds.center, Vector3.up*0.1f, out hit, 0.1f) &&  hit.collider.tag == "WaterSurface")
                return true;
            return false;    
        }
    }
}