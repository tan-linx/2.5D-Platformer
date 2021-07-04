using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    public class ArmTarget : MonoBehaviour
    {
        [SerializeField] private CharacterControl control;
        private float offsetX;

        void Start()
        {
            control.Animator.transform.localPosition = new Vector3(-0.179f, -0.978f, 0.45f);
            if (this.gameObject.name == "LeftArmMovement_target")
            {
                transform.eulerAngles = new Vector3(-78.593f,175.969f, -70.836f);
                offsetX = -0.1f;
            }
            if (this.gameObject.name == "RightArmMovement_target")
            {
                transform.eulerAngles = new Vector3(-114.815f, -373.045f, 292.98f);
                offsetX = 0f; 
            }
        }

        void Update()
        {
            if (control.IsGrabbingRope && control.currentHitCollider!=null)
            {
                transform.position = new Vector3(control.currentHitCollider.transform.position.x+offsetX, transform.position.y, control.currentHitCollider.transform.position.z);
            }
        }
    }
}