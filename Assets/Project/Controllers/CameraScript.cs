using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(30, 2, 0);
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    [SerializeField]
    private Transform waterSurface;

    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;

    void Start () 
    {
    	//Set the background color
    	GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1);

        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
    }

    void Update()
    {
        transform.LookAt(target);
        Vector3 targetedPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetedPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;

        if (transform.position.y < waterSurface.position.y)
        {
            changeCameraUnderWater();
            //RenderSettings.fog = true;
            //RenderSettings.fogColor = new Color(0.16f, 0.25f, 0.3f, 1f);
            //RenderSettings.fogDensity = 0.05f; //0.04
        }
        else
        {
            offset = new Vector3(25, 2, 0);

            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
        }
    }

    private void changeCameraUnderWater() 
    {
        offset = new Vector3(offset.x, 0f, offset.z);
    }
}
