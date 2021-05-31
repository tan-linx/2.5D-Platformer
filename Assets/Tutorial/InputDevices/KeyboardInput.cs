using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    /**
        it is mandatory to set the Character Controller with set Method
        Note: Might have to change this
    **/

    public class KeyboardInput : CharacterControl
    {
        // Start is called before the first frame update

        private CharacterControl controller;

        // Update is called once per frame
        void Update()
        {
            
            if (controller == null) return;
            if (Input.GetKey(KeyCode.D)) 
            {
                controller.MoveRight = true;
            } 
            else 
            {
                controller.MoveRight = false;
            }

            if (Input.GetKey(KeyCode.A)) 
            {
                controller.MoveLeft = true;
            } 
            else 
            {
                controller.MoveLeft = false;
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                controller.Jump = true;
            } 
            else
            {
                controller.Jump = false;
            }
            
        }

        public CharacterControl Controller {
            get => this.controller; 
            set => this.controller = value;
        }
    }
}