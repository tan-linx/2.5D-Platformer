using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    public class RopeTrigger : MonoBehaviour
    {
        [SerializeField]
        private Transform ropeParent;
        private bool grabbingRope = false; 
        
        void Awake() 
        {
            if (ropeParent != null) transform.SetParent(ropeParent, false); //Or find Ropepart(16)
        }

        private void OnTriggerEnter(Collider other)
        {
            if (checkTags(other)) return;
            Debug.Log(other);
            CharacterControl control = other.gameObject.GetComponentInParent<CharacterControl>();
            control.currentHitCollider = GetComponentInParent<CapsuleCollider>();
            grabbingRope = true;
            control.grabbingRope = grabbingRope;
            //GetComponentInParent<CapsuleCollider>().isTrigger = false;
        }

        private bool checkTags(Collider other) {
            if (other.gameObject.tag == "Ledge" 
            || grabbingRope) return true;  
            return false;
        }
    }
}
