using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    public class TargetScript : MonoBehaviour
    {
        [SerializeField]
        private CharacterControl control;
        private float force; 

        void Start()
        {
            transform.Translate(Vector3.forward*0.5f);
        }

        void Update()
        {
            force = control.RIGID_BODY.velocity.y;
            //Debug.Log("Something is happening" + control.RIGID_BODY.velocity);
            transform.Translate(Vector3.up*0.1f);
        }
    }
}   