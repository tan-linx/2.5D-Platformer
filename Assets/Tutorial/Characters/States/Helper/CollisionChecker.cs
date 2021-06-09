using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    public class CollisionChecker : MonoBehaviour
    {
        /**
        TODO: remove the debugs
        **/
        //class cant have static methods        
    /*
        float maxClimbAngle = 70;
        RaycastHit hit; //to check distance from hit

        //ref: 
        //https://github.com/SebLague/2DPlatformer-Tutorial/blob/master/Platformer%20E04/Assets/Scripts/Controller2D.cs
        void ClimbSlope(CharacterControl control) 
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            
            //most Right hit or most left hit
            Vector3 dir = Vector3.forward;
            if (control.MoveLeft)
            {
                dir = Vector3.back;
            }
            if (Physics.Raycast(control.transform.position, 
                    raydir*collider.radius, out hit))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                if  (slopeAngle <= maxClimbAngle) {
                    
                    float gradientZ = 1f;
                    float gradientY = Mathf.Tan(slopeAngle);
                    //direction is influenced by gravity
                    Vector3 dir = (0, gradientY, gradientZ); 
                    MovePosition(control.RIGID_BODY.position
                    + dir);   
                }
            }
        }

        void PhysicsSlope(ref Vector3 velocity, float slopeAngle)
        {
            float moveDistance = Mathf.Abs(velocity.x);
            float climbVelocityY = Mathf.Sin(slopeAngle*Mathf.Deg2Rad) * moveDistance;

            if (velocity.y <= climbVelocityY) {
                velocity.y = climbVelocityY;
			    velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            }
        }
        
        /*
	    void CalculateRaySpacing(CharacterControl control) {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
	    	horizontalRaySpacing = (collider.radius*2)/horizontalRayCount;
	    	verticalRaySpacing = collider.height/verticalRayCount;
	    }*/
    }
}
