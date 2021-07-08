using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author Tanja Schlanstedt></author>
public class LineRendererScript : MonoBehaviour
{
    private LineRenderer lr; 
    private CapsuleCollider col;
    private Transform pos1; 
    private Transform pos2; 
    private const string firstRopePartName = "RopeStart"; 
    private const string lastRopePartName = "RopeEnd"; 
    private bool exit = false;
    
    void Start()
    {
        
        col = GetComponentInParent<CapsuleCollider>();
        lr = GetComponentInParent<LineRenderer>();
        lr.positionCount = 2;
        pos1 = col.transform;
        if (col.transform.childCount>0) 
        {
            pos2 = col.transform.GetChild(0).gameObject.transform;
        } 
        else 
        {
            exit = true;
        }
        lr.startWidth = 0.07f;
        lr.endWidth = 0.07f;
    }

    void FixedUpdate()
    {
        if (!exit)
        {
            if (col.name == firstRopePartName)
            {
                lr.SetPosition(0, new Vector3(pos1.position.x, 
                                            pos1.position.y + col.bounds.extents.y,
                                            pos1.position.z));
                lr.SetPosition(1, pos2.position);
                return;
            } 
            if (pos2 && pos2.name == lastRopePartName)
            {
                lr.SetPosition(0, pos1.position);
                lr.SetPosition(1, new Vector3(pos2.position.x, 
                                            pos2.position.y - col.bounds.extents.y,
                                            pos2.position.z));
                return;
            }
            lr.SetPosition(0, pos1.position);
            lr.SetPosition(1, pos2.position);
            return;
        }

    }
}
