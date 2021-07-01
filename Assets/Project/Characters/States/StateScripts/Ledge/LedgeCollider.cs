using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    public class LedgeCollider : MonoBehaviour
    {
        public List<GameObject> CollidedObjects = new List<GameObject>();
    
        private void OnTriggerEnter(Collider other)
        {
            if (!CollidedObjects.Contains(other.gameObject) && !IsBodyPart(other) && !IsIgnoredPart(other))
            {
                CollidedObjects.Add(other.gameObject);
            }  
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (CollidedObjects.Contains(other.gameObject))
            {
                CollidedObjects.Remove(other.gameObject);
            }
    
        }

        private bool IsBodyPart(Collider col)
        {
            CharacterControl control = GetComponentInParent<CharacterControl>();
            if (control.gameObject == col.gameObject 
                || control.Animator.gameObject == col.gameObject || col.gameObject.tag == "Player")
            {
                return true;
            }
            /*if (control.RagdollParts.Contains(col))
            {
                return true;
            }*/
            return false;
        }

        protected bool IsIgnoredPart(Collider col)
        {
            switch (col.gameObject.tag)
            {
                case "Rope":
                    return true;
                case "LadderDown":
                    return true;
                case "Water":
                    return true;     
                default: 
                    return false;
            }
        }
    }
}
