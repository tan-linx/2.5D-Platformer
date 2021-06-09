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
        protected int fallVelocityHash;

        
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
            fallVelocityHash = Animator.StringToHash("FallVelocity");
        }

        /// <summary>method <c>CheckColliders in Front</c> Checks if Object is on the front
        /// and does a certain Animation when a specific Tag is on the Object which it collided with.</summary>
        public void HandleColliderData(CharacterControl control, Animator animator)
        {
            float offset = 0.15f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Collider[] hitColliders = Physics.OverlapBox(
                control.transform.position, 
                new Vector3(collider.radius, collider.height+offset, collider.radius+offset),  
                control.transform.rotation); 
            foreach(Collider hitCollider in hitColliders) {
                control.HitCollider = hitCollider; 
                switch(hitCollider.gameObject.tag) 
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


         /// <summary>method <c>CheckFront</c> Checks if Object is on the front
         /// Draws 4 Rays to check the Front</summary>
        public bool CheckFront(CharacterControl control)
        {  
            RaycastHit hit;
            //float offset = 0.04f;
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();

            float verticalRayCount = 5;
	    	float verticalRaySpacing = collider.height/verticalRayCount;
            float maxRayLength = collider.radius;

            Vector3 dir = Vector3.forward;
            if (control.MoveLeft) dir = Vector3.back;
            Vector3 rayOrigin = control.transform.position + Vector3.up*(collider.height/2);
            for (int i=0; i<verticalRayCount; i++)
            {  
                Debug.DrawRay(rayOrigin, 
                    dir*maxRayLength, Color.green);                
                if (Physics.Raycast(rayOrigin, dir, out hit, maxRayLength)) 
                { 
                    if (!IsRagdollPart(control, hit.collider))
                    {
                        return true;
                    }
                }
                rayOrigin += Vector3.down*verticalRaySpacing;    
            }  
            return false;
        }


        private bool IsRagdollPart(CharacterControl control, Collider col)
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

        /// <summary>method <c>CheckFront</c> Checks if Collider collides with something up</summary>
        public bool CheckHead(CharacterControl control)
        {  
            CapsuleCollider collider = control.GetComponent<CapsuleCollider>();
            Vector3 rayOrigin = control.transform.position; 
            Vector3 dir = new Vector3(0, collider.height-collider.height/3, 0);

            if (control.MoveRight) 
                rayOrigin += Vector3.forward*collider.radius;
            if (control.MoveLeft)
                rayOrigin -= Vector3.forward*collider.radius;
                Debug.DrawRay(rayOrigin, dir, Color.green);
            return Physics.Raycast(rayOrigin,  dir); 
        } 
    }
}

