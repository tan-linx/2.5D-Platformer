using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment
{
    public class ClimbLadderDown : MonoBehaviour
    {
        //to dermine the direction the player is facing the ladder
        [SerializeField]
        private bool faceLadderForward;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") 
            {
                CharacterControl control = other.GetComponentInParent<CharacterControl>();
                control.IsClimbDownLadder = true;    
                // because we only have one ladder to climb down
                if (faceLadderForward) control.currentHitDirection = HitDirection.FORWARD;
                else control.currentHitDirection = HitDirection.BACK;
                control.currentHitCollider = GetComponent<BoxCollider>();             
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player") 
            {
                CharacterControl control = other.GetComponentInParent<CharacterControl>();
                control.IsClimbDownLadder = false;            
            }
        }
    }
}