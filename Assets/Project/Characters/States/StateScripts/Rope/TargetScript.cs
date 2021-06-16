using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    public class TargetScript : MonoBehaviour
    {
        [SerializeField]
        private CharacterControl control;

        // Start is called before the first frame update
        void Start()
        {

        }

        //https://docs.unity3d.com/ScriptReference/Animator.GetCurrentAnimatorStateInfo.html
        // Update is called once per frame
        void Update()
        {
            if (control.currentHitCollider != null)   
            {
                if (control.currentHitCollider.tag == "Rope")
                {
                    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,
                    control.currentHitCollider.gameObject.transform.position.y, control.currentHitCollider.gameObject.transform.position.z);
                }
            }
        }
    }
    }   