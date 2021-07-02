using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            verticalRayCount = 5;
            distanceMovedUp = 0f;
        }

        /// <summary>method <c>CheckFront</c> Checks if Object is on the front
        /// Draws 4 Rays to check the Front</summary>
        public bool CheckFront(CharacterControl control, Vector3 dir)
        {  
            RaycastHit hit;
            //float offset = 0.04f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
	    	float verticalRaySpacing = collider.bounds.size.y/verticalRayCount;
            float maxRayLength = collider.radius;
            Vector3 rayOrigin = collider.bounds.center + Vector3.up*(collider.bounds.extents.y);
            for (int i=0; i<verticalRayCount; i++)
            {  
                //Debug.DrawRay(rayOrigin, dir*maxRayLength, Color.green);                
                if (Physics.Raycast(rayOrigin, dir, out hit, maxRayLength) 
                    && !IsRagdollPart(control, hit.collider)
                    && !IsIgnoredPart(hit.collider)) 
                { 
                    return true;
                }
                rayOrigin += Vector3.down*verticalRaySpacing;    
            }  
            return false;
        }

        protected bool IsRagdollPart(CharacterControl control, Collider col)
        {
            foreach(Collider c in control.RagdollParts)
            {
                if (c.gameObject == col.gameObject)
                {
                    return true; 
                }
            }
            return false;
        }

        protected bool IsIgnoredPart(Collider col)
        {
            switch (col.gameObject.tag)
            {
                case "Rope":
                    return true;
                case "LadderDown":
                    return true;
                default: 
                    return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////The following is important for the rope swing///////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>method <c>Check Collider is in Front </c>  Especially to Check e.g. for Rope. </summary>
        protected string GetColliderTag(CharacterControl control, Vector3 dir) 
        {
            RaycastHit hit;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            if (Physics.SphereCast(collider.bounds.center+Vector3.up*(collider.bounds.extents.y), 
            collider.bounds.extents.z, dir, out hit, collider.bounds.extents.z))
            { 
                if (!IsRagdollPart(control, hit.collider))
                {
                    control.currentHitDirection = dir; //TODO:
                    control.currentHitCollider = hit.collider;
                    return hit.collider.gameObject.tag;
                }
            }
            return "";
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
    }
}

