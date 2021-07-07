using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment {
    public class Pushable : MonoBehaviour
    {
        private bool pushable;

        private void Awake()
        {
            pushable = true;
        }

        private void Update() 
        {
            BoxCollider col = gameObject.GetComponent<BoxCollider>();
            Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents);
            foreach (Collider collider in colliders)
            {
                if (collider.name == "Box")
                {
                    pushable = false;
                } 
                else 
                {
                    pushable = true;
                }
            }
        }

        public bool IsPushable
        {
            get { return pushable; }
        }
    }
}   