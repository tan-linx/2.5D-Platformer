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
        protected int groundedHash;
        protected int transitionHash;
        protected int crashHash; 
        protected int hangingHash;


        protected int verticalRayCount;

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
            groundedHash = Animator.StringToHash("Grounded");
            transitionHash = Animator.StringToHash("ForceTransition");
            crashHash = Animator.StringToHash("Crash");
            hangingHash = Animator.StringToHash("Hanging");
            verticalRayCount = 5;
        }

        /// <summary>method <c>CheckColliders in Front</c> Checks if Object is on the front
        /// and does a certain Animation when a specific Tag is on the Object which it collided with.</summary>
        public void HandleColliderData(CharacterControl control, Animator animator)
        {
          //  float offset = 0.15f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Collider[] hitColliders = Physics.OverlapBox(
                collider.bounds.center, collider.bounds.extents,  
                control.transform.rotation); 
            foreach(Collider hitCol in hitColliders) 
            {
                control.currentHitCollider = hitCol; 
                if (!IsRagdollPart(control, hitCol))
                 {
                    switch(hitCol.gameObject.tag) 
                    {   
                        case "CrouchObstacle":
                            animator.SetBool(crouchHash, true);
                            return;      
                        case "PushObstacle":
                            animator.SetBool(pushHash, true);
                            return;
                    }
                }
            }  
        }


         /// <summary>method <c>CheckFront</c> Checks if Object is on the front
         /// Draws 4 Rays to check the Front</summary>
        public bool CheckFront(CharacterControl control)
        {  
            RaycastHit hit;
            //float offset = 0.04f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
	    	float verticalRaySpacing = collider.bounds.size.y/verticalRayCount;
            float maxRayLength = collider.radius;

            Vector3 dir = Vector3.forward;
            if (control.MoveLeft) dir = Vector3.back;
            Vector3 rayOrigin = collider.bounds.center + Vector3.up*(collider.bounds.extents.y);
            for (int i=0; i<verticalRayCount; i++)
            {  
                Debug.DrawRay(rayOrigin, 
                    dir*maxRayLength, Color.green);                
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

        private bool IsIgnoredPart(Collider col)
        {
            switch (col.gameObject.tag)
            {
                case "Rope":
                    return true;
                default: 
                    return false;
            }
        }

        /// <summary>method <c>CheckFront</c> Checks if Collider collides with something up</summary>
        public bool CheckHead(CharacterControl control)
        {  
            RaycastHit hitInfo;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = collider.bounds.center; 
            Vector3 dir = Vector3.up;
            float maxRayLength = collider.bounds.extents.y;
            Debug.DrawRay(rayOrigin, dir, Color.green);
            if(Physics.Raycast(rayOrigin,  dir, out hitInfo, maxRayLength) && !IsRagdollPart(control, hitInfo.collider))
                return true; 
            return false;    
        } 
    }
}

