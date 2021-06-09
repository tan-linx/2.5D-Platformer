using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer_Assignment
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool IsGrabbingLedge;
        CharacterControl control;

        /**
        pos = (0, 1.266,1.02)
        scale = (1.97, 0.19, 0.32)
        **/
        private LedgeCollider lowerCollider;
        
        /**
        pos = (0, 1.47,1.02)
        **/
        [SerializeField]
        private LedgeCollider upperCollider;

        private void Start() 
        {
            IsGrabbingLedge = false;
            control = GetComponentInParent<CharacterControl>();
            lowerCollider = this.gameObject.GetComponent<LedgeCollider>();
        }

        private void FixedUpdate() 
        {
            if (lowerCollider.CollidedObjects.Count == 0)
            {
                IsGrabbingLedge = false;
            }
            foreach(GameObject obj in lowerCollider.CollidedObjects) 
            {
                if (!upperCollider.CollidedObjects.Contains(obj))
                {
                    IsGrabbingLedge = true;
                    break;
                }
                else
                {
                    IsGrabbingLedge = false;
                }
            }
            Debug.Log(IsGrabbingLedge);
        }

        public LedgeCollider LowerCollider
        {
            get { return lowerCollider; }
        }
    }
}