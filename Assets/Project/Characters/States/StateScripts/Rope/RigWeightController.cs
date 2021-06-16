using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Platformer_Assignment {
    public class RigWeightController : MonoBehaviour
    {
        [SerializeField]
        private CharacterControl control;
        private Rig rig;

        // Start is called before the first frame update
        void Start()
        {
            rig = GetComponent<Rig>();
        }

        //https://docs.unity3d.com/ScriptReference/Animator.GetCurrentAnimatorStateInfo.html
        // Update is called once per frame
        void Update()
        {
            if (control.currentHitCollider != null) 
            {
                if (control.currentHitCollider.tag == "Rope")
                {
                    rig.weight = 100;
                }
                else {
                    rig.weight = 0;
                }
            }
        }
    }
}