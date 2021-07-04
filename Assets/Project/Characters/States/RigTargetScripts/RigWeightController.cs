using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

///<author Tanja Schlanstedt></author>
namespace Platformer_Assignment {
    public class RigWeightController : MonoBehaviour
    {
        [SerializeField] private CharacterControl control;
        private Rig rig;

        void Start()
        {
            rig = GetComponent<Rig>();
        }

        void Update()
        {
            if (!control.IsGrabbingRope)
            {
                rig.weight = 0;
            }
            else 
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
}