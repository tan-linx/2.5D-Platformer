using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Platformer_Assignment 
{
    public class FootTarget : MonoBehaviour
    {
        [SerializeField]
        private CharacterControl control;
        private Rig rig;

        void Start()
        {
            rig = GetComponent<Rig>();
        }

        void Update()
        {
            Debug.Log(control.Animator.GetCurrentAnimatorStateInfo(0).IsName("Climb Rope"));
            if (control.Animator.GetCurrentAnimatorStateInfo(0).IsName("Climb Rope"))
            {
                //transform.eulerAngles = new Vector3(-114.815f, -373.045f, 292.98f);
                rig.weight = 100;
            } else 
            {
                rig.weight = 0;
            }
        }
    }
}   