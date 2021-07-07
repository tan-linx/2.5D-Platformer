using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    //https://www.youtube.com/watch?v=pKSUhsyrj_4&t=165s
    [SerializeField]
    private GameObject ropePartPrefab;

    int count = 3;

    private float distanceBetweenPart;

    void Awake()
    {
        distanceBetweenPart = 0.21f;
        GameObject parentRopePart = ropePartPrefab;
        for (int i=0; i<count; i++)
        {        
            GameObject tmp = Instantiate<GameObject>(ropePartPrefab, parentRopePart.transform, true);
            tmp.transform.position = new Vector3(ropePartPrefab.transform.position.x, ropePartPrefab.transform.position.y - distanceBetweenPart*(i+1),
                ropePartPrefab.transform.position.z);
            tmp.GetComponent<ConfigurableJoint>().connectedBody = parentRopePart.GetComponent<Rigidbody>();
            //tmp.SetParent(parentRopePart.transform);
            tmp.name = "RopePart" + i;
            if (i == count) 
            {
                tmp.name = "RopeEnd";
            } else
            {
                tmp.name = "RopePart " + i;
            }
            parentRopePart = tmp;
       }
    }
}

