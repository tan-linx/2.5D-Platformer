using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public LineRenderer lr; 
    private CapsuleCollider col;
    private Transform pos1; 
    private Transform pos2; 
    private bool exit = false;
    
    void Start()
    {
        
        col = GetComponentInParent<CapsuleCollider>();
        lr = GetComponentInParent<LineRenderer>();
        lr.positionCount = 2;
        pos1 = col.gameObject.transform;
        if (col.gameObject.transform.childCount!=0) 
        {
            pos2 = col.gameObject.transform.GetChild(0).
                gameObject.transform;
        } else 
        {
            exit = true;
        }
        lr.startWidth = 0.07f;
        lr.endWidth =0.07f;
    }

    void FixedUpdate()
    {
        if (!exit)
        {
            lr.SetPosition(0, pos1.position);
            lr.SetPosition(1, pos2.position);
        }
    }
}
