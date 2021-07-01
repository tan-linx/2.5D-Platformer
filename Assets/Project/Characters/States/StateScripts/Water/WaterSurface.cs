using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Platformer_Assignment {
public class WaterSurface : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponentInParent<CharacterControl>())
        {
            CharacterControl control = collider.GetComponentInParent<CharacterControl>();
            //control.hitWaterSurface = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponentInParent<CharacterControl>())
        {
            CharacterControl control = collider.GetComponentInParent<CharacterControl>();
            //control.hitWaterSurface = false;
        }
    }
}
}