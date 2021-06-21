using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Platformer_Assignment
{
    public class ArmTarget : MonoBehaviour
    {
        [SerializeField]
        private CharacterControl control;
        
        void Start()
        {
        }

        void Update()
        {
            if (control.grabbingRope && control.currentHitCollider!=null)
            {
                transform.position = control.currentHitCollider.transform.position;
            }
        }
    }
}