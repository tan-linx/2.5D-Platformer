using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_Assignment {
    public class WaterDeadTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {

            if (col.tag == "Player")
            {
                col.GetComponentInParent<CharacterControl>().Dead = true;
            }
        }
    }
}