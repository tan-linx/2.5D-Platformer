using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment
{
    public class ClimbLadderDown : MonoBehaviour
    {
        [SerializeField]
        private bool faceLadderForward;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") 
            {
                CharacterControl control = other.GetComponentInParent<CharacterControl>();
                control.climbDownLadder = true;    
                // because we only have one ladder to climb down
                if (faceLadderForward) control.currentHitDirection = Vector3.forward;
                else control.currentHitDirection = Vector3.back;
                control.currentHitCollider = GetComponent<BoxCollider>();             
            }
        }
    }
}