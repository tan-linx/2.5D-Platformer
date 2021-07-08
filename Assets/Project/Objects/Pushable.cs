using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment {

    /// <summary>Class <c>Pushable</c>
    /// Determines whether object is pushable.
    /// Using OverlapBox instead of colliders
    ///on moveable objects because rigidbodies cause weird behaviour.</summary>
    public class Pushable : MonoBehaviour
    {
        [SerializeField]
        Animator anim;
        [SerializeField]
        private LayerMask m_LayerMask;
        public bool moveable;

        void Awake()
        {
            moveable = true;
        }

        void FixedUpdate() 
        {
            Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale/2, 
                                                        Quaternion.identity, m_LayerMask);
            if (colliders.Length > 0) 
            {
                for(int i = 0; i<colliders.Length; i++)
                {
                    if (IsValidOverlap(colliders[i]))
                    {
                        moveable = false;
                        return;
                    }
                }
                moveable = true;
            }
            else 
                moveable = true;
        }

        private bool IsValidOverlap(Collider collider)
        {
            return collider.tag != "Player" 
                        && collider.gameObject != this.gameObject
                        && collider.gameObject != anim.gameObject 
                        && collider.tag != "Ledge";
        }

        public bool Moveable
        {
            get { return moveable; }
        }
    }
}   