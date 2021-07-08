using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* based on https://www.youtube.com/watch?v=z8gLOWpLYak&list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6&index=53
   https://drive.google.com/drive/folders/1sQnid4-4EMlJgX_DZ6YUAxFbGeo6mXUE
    modified: 
    -IsBodyPart()
    -IsignoredPart()
*/
namespace Platformer_Assignment
{
    public class LedgeCollider : MonoBehaviour
    {
        public List<GameObject> CollidedObjects = new List<GameObject>();
    
        private void OnTriggerEnter(Collider other)
        {
            if (!CollidedObjects.Contains(other.gameObject) 
            && !IsBodyPart(other) 
            && !IsIgnoredPart(other))
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
                || control.Animator.gameObject == col.gameObject 
                || col.gameObject.tag == "Player")
            {
                return true;
            }
            return false;
        }

        private bool IsIgnoredPart(Collider col)
        {
            switch (col.gameObject.tag)
            {
                case "Rope":
                    return true;
                case "Ladder":
                    return true;      
                case "LadderDown":
                    return true;           
                case "Ignored":
                    return true;      
                default: 
                    return false;
            }
        }
    }
}
