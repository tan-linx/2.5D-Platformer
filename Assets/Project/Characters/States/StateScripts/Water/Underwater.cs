using UnityEngine;
using System.Collections;
 
namespace Platformer_Assignment {
    //http://wiki.unity3d.com/index.php?title=Underwater_Script#C.23_-_Underwater.cs
    public class Underwater : MonoBehaviour {
    
    	//This script enables underwater effects. Attach to main camera.
        //Define variable
        [SerializeField]
        private Transform waterSurface;
    
        //The scene's default fog settings
        private bool defaultFog = RenderSettings.fog;
        private Color defaultFogColor = RenderSettings.fogColor;
        private float defaultFogDensity = RenderSettings.fogDensity;
        private Material defaultSkybox = RenderSettings.skybox;
        private Material noSkybox;
    
        void Start () {
    	    //Set the background color
    	    GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1);
        }
    
        void Update () {
            if (transform.position.y < waterSurface.position.y)
            {
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
                RenderSettings.fogDensity = 0.04f;
                RenderSettings.skybox = noSkybox;
            }
            else
            {
                RenderSettings.fog = defaultFog;
                RenderSettings.fogColor = defaultFogColor;
                RenderSettings.fogDensity = defaultFogDensity;
                RenderSettings.skybox = defaultSkybox;
            }
        }
    }
    }   