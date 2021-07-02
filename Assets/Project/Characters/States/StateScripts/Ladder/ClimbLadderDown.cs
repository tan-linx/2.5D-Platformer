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
                if (faceLadderForward) control.currentHitDirection = HitDirection.FORWARD;
                else control.currentHitDirection = HitDirection.BACK;
                control.currentHitCollider = GetComponent<BoxCollider>();             
            }
        }
    }
}