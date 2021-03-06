using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* based on https://drive.google.com/drive/folders/1URSCwsxxxJsWg9ctpTEWFCbM3KhOH4mO
 modified:  added hashes for state names
 architecture based on the tutorial,
 all methods from me except for OnEnter(), UpdateAbility(), OnExit() */
 
namespace Platformer_Assignment { 
    public abstract class StateData : ScriptableObject 
    {
        protected int jumpHash;
        protected int crouchHash;
        protected int moveHash;
        protected int pushHash;
        protected int pullHash;
        protected int groundedHash;
        protected int transitionHash;
        protected int crashHash; 
        protected int hangingHash;
        protected int climbHash;
        protected int coverLeftHash;
        protected int coverRightHash;
        protected int fallingHash;

        protected int verticalRayCount;
        protected float distanceMovedUp;

        public abstract void OnEnter(CharacterState characaterState, 
        Animator animator, AnimatorStateInfo stateInfo);
        public abstract void UpdateAbility(CharacterState characaterState, 
        Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnExit(CharacterState characaterState, 
        Animator animator, AnimatorStateInfo stateInfo);

        void Awake() 
        {
            jumpHash = Animator.StringToHash("Jump");
            crouchHash = Animator.StringToHash("Crouch");
            moveHash = Animator.StringToHash("Move");
            pushHash = Animator.StringToHash("Push");
            pullHash = Animator.StringToHash("Pull");
            groundedHash = Animator.StringToHash("Grounded");
            transitionHash = Animator.StringToHash("ForceTransition");
            crashHash = Animator.StringToHash("Crash");
            hangingHash = Animator.StringToHash("Hanging");
            climbHash = Animator.StringToHash("Climb");
            coverLeftHash = Animator.StringToHash("CoverLeft");
            coverRightHash = Animator.StringToHash("CoverRight");
            fallingHash = Animator.StringToHash("Falling");
            verticalRayCount = 5;
            distanceMovedUp = 0f;
        }

        /// <summary>method <c>CheckFront</c> Checks if Object is on the front
        /// Draws 4 Rays to check the Front</summary>
        public bool CheckFront(CharacterControl control, Vector3 dir)
        {  
            RaycastHit hit;
            float offset = 0.27f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
	    	float verticalRaySpacing = collider.bounds.size.y/verticalRayCount;
            float maxRayLength = collider.bounds.extents.z + offset;
            Vector3 rayOrigin = collider.bounds.center + Vector3.up*(collider.bounds.extents.y);
            for (int i=0; i<verticalRayCount; i++)
            {  
                //Debug.DrawRay(rayOrigin, dir*maxRayLength, Color.green);                
                if (Physics.Raycast(rayOrigin, dir, out hit, maxRayLength) 
                    && !IsIgnoredPart(hit.collider)) 
                { 
                    return true;
                }
                rayOrigin += Vector3.down*verticalRaySpacing;    
            }  
            return false;
        }

        /// <summary>method <c>CheckFront</c> Overrides method from inherited class because 
        /// the raycast shoots lower in a crouch position. </summary>
        protected bool CrouchCheckFront(CharacterControl control, Vector3 dir)
        {
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            float maxRayLength = collider.bounds.size.z;
            if(Physics.Raycast(collider.bounds.center, dir, maxRayLength*2))
                return true;
            return false;
        }

        /// <summary>method <c>CheckHead</c> Checks if collider collides with something above</summary>
        protected bool CheckHead(CharacterControl control)
        {  
            RaycastHit hitInfo;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = collider.bounds.center; 
            Vector3 dir = Vector3.up;
            float maxRayLength = collider.bounds.extents.y;
            if(Physics.Raycast(rayOrigin,  dir, out hitInfo, maxRayLength))
                return true; 
            return false;    
        } 

        protected bool IsIgnoredPart(Collider col)
        {
            switch (col.gameObject.tag)
            {
                case "Player":
                    return true;
                case "Rope":
                    return true;
                case "LadderDown":
                    return true;
                case "Ledge":
                    return true;  
                case "Ignored":
                    return true;      
                default: 
                    return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////The following is important for the rope swing///////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>method <c>Check Collider is in Front </c>  Especially to Check e.g. for Rope. </summary>
        protected bool IsRopeCollider(CharacterControl control, Vector3 dir) 
        {
            RaycastHit hit;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            if (Physics.SphereCast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y), 
            collider.bounds.extents.z, dir, out hit, collider.bounds.extents.z))
            { 
                
                if (hit.collider.tag == "Rope")
                {
                    control.currentHitDirection = VectorToHitDirection(dir); 
                    control.currentHitCollider = hit.collider;
                    return true;
                }
            }
            return false;
        }

        /// <summary>method <c>SetTriggerRopeColliders</c> Sets the Trigger of an Object and all his children
        ///based on the argument on.</summary>
        protected void SetTriggerRopeColliders(Transform parent, bool on)
        {
            foreach(Transform child in parent) 
            {
                if (child.GetComponent<CapsuleCollider>() != null) 
                {
                    child.GetComponent<CapsuleCollider>().isTrigger = on;
                    SetTriggerRopeColliders(child, on);
                }
            }
        }

         /// <summary>method <c>IsKinematicRope</c> Sets the IsKinematic Option of Object and all 
        ///children.</summary>
        protected void IsKinematicRope(Transform parent, bool on)
        {
            foreach(Transform child in parent) 
            {
                if (child.GetComponent<Rigidbody>() != null) 
                {
                    child.GetComponent<Rigidbody>().isKinematic = on;
                    IsKinematicRope(child, on);
                }
            }
        }

        protected HitDirection VectorToHitDirection(Vector3 dir)
        {
            if (dir == Vector3.forward)
                return HitDirection.FORWARD;
            else if (dir == Vector3.back)
                return HitDirection.BACK; 
            else 
                return HitDirection.None;    

        }
    }
}

