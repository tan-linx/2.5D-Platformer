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


        /// <summary>method <c>CheckFront</c> Checks if Collider collides with something up</summary>
        //if user moves right check from the right most bodypart
        //i
        public static bool CheckHead(CharacterControl control)
        {  
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = control.transform.position; //CheckHead 
            Vector3 dir = new Vector3(0, collider.height-collider.height/3, 0);

            if (control.MoveRight) 
                rayOrigin += Vector3.forward*collider.radius;
            if (control.MoveLeft)
                rayOrigin -= Vector3.forward*collider.radius;
                Debug.DrawRay(rayOrigin, dir, Color.green);
            return Physics.Raycast(rayOrigin,  dir); 
        }

        /// <summary>method <c>IsGrounded</c> Checks whether Collider is grounded</summary>
        //checking from left to right
        public static bool IsGrounded(CharacterControl control)
        { 
            //when velocity around 0 
            if (control.RIGID_BODY.velocity.y > -0.001f 
                && control.RIGID_BODY.velocity.y <= 0f)
            {
                return true;
            }     
            //when rigid_body is falling
            if (control.RIGID_BODY.velocity.y <= 0f) 
            {
                CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
                Vector3 rayOrigin = control.transform.position + Vector3.back*collider.radius;

                float horizontalRayCount = 4;
                float horizontalRaySpacing = (collider.radius*2)/horizontalRayCount;
                for (int i=0; i<horizontalRayCount; i++) 
                {
                    Debug.DrawRay(rayOrigin, 
                        Vector3.down*0.7f, Color.green);
                    if (Physics.Raycast(rayOrigin, 
                        Vector3.down*0.7f)) 
                    {
                        return true;
                    }
                    rayOrigin += Vector3.forward*horizontalRaySpacing;
                }
            }
            return false; 
        }   

      /// <summary>method <c>CheckFront</c> Checks if Object is on the front</summary>
      //draws rays from top to bottom
        public static bool CheckFront(CharacterControl control)
        {  
            
            float offset = .15f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();

            float verticalRayCount = 4;
	    	float verticalRaySpacing = collider.height/verticalRayCount;

            float rayLength = collider.radius;
            Vector3 dir = Vector3.forward;
            if (control.MoveLeft)
                dir = Vector3.back;

            Vector3 rayOrigin = control.transform.position + Vector3.up*collider.height;
            for (int i=0; i<verticalRayCount; i++)
            {  
                Debug.DrawRay(rayOrigin, 
                    dir * rayLength, Color.green);
                if (Physics.Raycast(rayOrigin, 
                    dir * rayLength,
                    collider.radius + offset))
                    return true; 
                rayOrigin -= Vector3.up*verticalRaySpacing;    
            }  
            return false;
        }
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
