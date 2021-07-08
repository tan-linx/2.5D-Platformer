using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://www.youtube.com/watch?v=z8gLOWpLYak&list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6&index=53
// modified: CalculateDirection() to find out in which direction a ledge was hit, 
// so the animation offset and teleportion suits the character control
namespace Platformer_Assignment
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool IsGrabbingLedge;
        private CharacterControl control;
        private LedgeCollider lowerCollider;

        [SerializeField] private LedgeCollider upperCollider;

        void Awake() 
        {
            IsGrabbingLedge = false;
            control = GetComponentInParent<CharacterControl>();
            lowerCollider = GetComponent<LedgeCollider>();
        }

        void FixedUpdate() 
        {
            if (lowerCollider.CollidedObjects.Count == 0)
            {
                IsGrabbingLedge = false;
            }
            foreach(GameObject obj in lowerCollider.CollidedObjects) 
            {
                if (!upperCollider.CollidedObjects.Contains(obj))
                {
                    CalculateDirection(control.transform, obj.transform);
                    IsGrabbingLedge = true;
                    break;
                }
                else
                {
                    IsGrabbingLedge = false;
                }
            }
        }

        public LedgeCollider LowerCollider
        {
            get { return lowerCollider; }
        }

        private void CalculateDirection(Transform source, Transform destination) 
        {
            float directionZ = destination.position.z - source.position.z;
            if (directionZ >= 0)
                control.currentHitDirection = HitDirection.FORWARD;
            else 
                control.currentHitDirection = HitDirection.BACK;  
        }
    }
}