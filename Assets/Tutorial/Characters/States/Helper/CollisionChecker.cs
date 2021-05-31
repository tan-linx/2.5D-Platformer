using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    public class CollisionChecker : MonoBehaviour
    {
         /// <summary>method <c>CheckFront</c> Checks if Object is on the front</summary>
        public static bool CheckFront(CharacterControl control)
        {  
             //when rigid_body is falling
            float offset = .1f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 dir;
            if (control.MoveRight)
                dir = new Vector3(0, 0, collider.radius);
            else 
                dir = new Vector3(0, 0, -collider.radius);
            return Physics.Raycast(control.transform.position, 
                    dir,
                    collider.radius + offset); //maybe add frontlayermask            return false;    
        }
    }
}
