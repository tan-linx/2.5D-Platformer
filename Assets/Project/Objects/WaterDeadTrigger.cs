using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
namespace Platformer_Assignment {
    
    /// <summary>Class <c>WaterDeadTrigger</c>
    /// Players dies when hitting the water</summary>
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