using UnityEngine;
using System.Collections;

// Composit and FOW-effect.
// This script creates fog of war and renders pixel-perfect texture to screen

public class FOW_Effect : MonoBehaviour
{
    // Effect settings
    public Shader shader;
    public int screenResolution = 512;

    // FOW settings
    public GameObject prefabFOW;

    RenderTexture fowTex;
    RenderTexture interactivesTex;
    RenderTexture silhouettesTex;
    RenderTexture environmentTex;
    RenderTexture groundTex;
    RenderTexture minimapPing;

    //public RenderTexture _fowTex;
    //public RenderTexture _interactivesTex;
    //public RenderTexture _silhouettesTex;
    //public RenderTexture _environmentTex;
    //public RenderTexture _groundTex;
    //public RenderTexture _minimapPing;

    public bool useSilhouette = true;
    public Color silhouetteColor;
    public bool useShadows = true;
    public Color shadowColor;
    public Vector2 shadowVector = new Vector2(-2, -1);

    Camera layerCam;
    public Material material;
    Vector2 oldResolution = Vector2.zero;
    int oldScreenResolution;

    void Start()
    {
        // Spawn fog of war prefab
        if (prefabFOW)
        {
            GameObject o = Instantiate(prefabFOW) as GameObject;
            o.transform.parent = transform.parent;
        }
        else
            Debug.Log("Fog of War-prefab was not set");

        material = new Material(shader);
        
        GameObject container = new GameObject();
        container.transform.parent = this.transform;
        container.AddComponent<Camera>();
        container.name = "subCamera";
        container.AddComponent<CameraUnloader>();
        layerCam = container.camera;
        layerCam.CopyFrom(camera);
        
        ResizeCheck();
        
        // Disable if we don't support image effects
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        // Disable the image effect if the shader can't run on the users graphics card
        if (!shader || !shader.isSupported)
            enabled = false;

        // Set camera to render with the shader FOW
        camera.RenderWithShader(shader, "FOW");        
    }

    void Update()
    {
        if (useShadows)
        {
            material.SetColor("_ShadowColor", shadowColor);
            material.SetVector("_ShadowVector", shadowVector * 0.01f);
        }
        else
            material.SetColor("_ShadowColor", Color.clear);

        if (useSilhouette)
            material.SetColor("_SilhouetteColor", silhouetteColor);
        else
            material.SetColor("_SilhouetteColor", Color.clear);

        ResizeCheck();
    }

    void OnPostRender()
    {
        // Change layer mask and render to specific render texture
        // Characters, entities and environment for correct layer order
        layerCam.cullingMask = 1 << LayerMask.NameToLayer("Interactives") | 1 << LayerMask.NameToLayer("Characters") | 1 << LayerMask.NameToLayer("Environment") | 1 << LayerMask.NameToLayer("UnbreakableEnvironment");
        layerCam.targetTexture = interactivesTex;
        layerCam.Render();
        //layerCam.targetTexture = _interactivesTex;
        //layerCam.Render();

        // Change layer mask and render to specific render texture
        // Visible fog area
        layerCam.cullingMask = 1 << LayerMask.NameToLayer("Characters");
        layerCam.targetTexture = silhouettesTex;
        layerCam.Render();
        //layerCam.targetTexture = _silhouettesTex;
        //layerCam.Render();

        // Change layer mask and render to specific render texture
        // Visible fog area
        layerCam.cullingMask = 1 << LayerMask.NameToLayer("Ground");
        layerCam.targetTexture = groundTex;
        layerCam.Render();
        //layerCam.targetTexture = _groundTex;
        //layerCam.Render();

        // Change layer mask and render to specific render texture
        // Blocks
        layerCam.cullingMask = 1 << LayerMask.NameToLayer("Environment") | 1 << LayerMask.NameToLayer("UnbreakableEnvironment") | 1 << LayerMask.NameToLayer("Default");
        layerCam.targetTexture = environmentTex;
        layerCam.Render();
        //layerCam.targetTexture = _environmentTex;
        //layerCam.Render();

        // Change layer mask and render to specific render texture
        // Fog texture overlay
        layerCam.cullingMask = 1 << LayerMask.NameToLayer("FogOfWar");
        layerCam.targetTexture = fowTex;
        layerCam.Render();
        //layerCam.targetTexture = _fowTex;
        //layerCam.Render();

        // http://answers.unity3d.com/questions/799941/blit-camera-targettexture-to-screen.html

        // Applies shader effect to current render (source) and sends to destination (screen)
        Graphics.Blit(null, null, material);
    }

    void ResizeCheck() // Sizes rendertextures with correct aspect ratio
    {
        // Failsafe
        if (screenResolution < 1)
            screenResolution = 1;
        if (screenResolution > 1024)
            screenResolution = 1024;

        if (new Vector2(Screen.width, Screen.height) != oldResolution || screenResolution != oldScreenResolution)
        {
            // Set old resolution to compare with next time
            oldResolution = new Vector2(Screen.width, Screen.height);
            oldScreenResolution = screenResolution;

            int resX;
            int resY;
            // Calculate aspect ratio
            float aspect = (float)Screen.width / (float)Screen.height;

            if (Screen.width > Screen.height) // If screen width is greater than screen height
            {
                //resY = (int)camera.orthographicSize * 64; // Set Y-resolution according to camera zoom (standard zoom 1=64 pixels wide/high)
                // Set correct resolution according to zoomOut on camera
                resY = screenResolution;
                if (resY >= 1024) // Failsafe in case camera orthographic size goes above 16 (which shouldn't happen)
                    resY = 1024;
                resX = (int)((float)resY * aspect);
            }
            else //  Else if screen height is greater than screen width
            {
                //resX = (int)camera.orthographicSize * 64; // Set X-resolution according to camera zoom (standard zoom 1=64 pixels wide/high)
                resX = screenResolution;
                if (resX >= 1024) // Failsafe in case camera orthographic size goes above 16 (which shouldn't happen)
                    resX = 1024;
                resY = (int)((float)resX / aspect);
            }

            // Changes size of rendertextures
            fowTex = new RenderTexture(resX, resY, -1, RenderTextureFormat.ARGBHalf);
            interactivesTex = new RenderTexture(resX, resY, -1, RenderTextureFormat.ARGBHalf);
            silhouettesTex = new RenderTexture(resX, resY, -1, RenderTextureFormat.ARGBHalf);
            environmentTex = new RenderTexture(resX, resY, -1, RenderTextureFormat.ARGBHalf);
            groundTex = new RenderTexture(resX, resY, -1, RenderTextureFormat.ARGBHalf);

            // Set shader textures
            material.SetTexture("_FowTex", fowTex);
            material.SetTexture("_Interactives", interactivesTex);
            material.SetTexture("_Silhouettes", silhouettesTex);
            material.SetTexture("_Environment", environmentTex);
            material.SetTexture("_Environment_1", environmentTex);
            material.SetTexture("_Ground", groundTex);

            fowTex.filterMode = FilterMode.Point;
            interactivesTex.filterMode = FilterMode.Point;
            silhouettesTex.filterMode = FilterMode.Point;
            environmentTex.filterMode = FilterMode.Point;
            groundTex.filterMode = FilterMode.Point;

            fowTex.anisoLevel = 0;
            interactivesTex.anisoLevel = 0;
            silhouettesTex.anisoLevel = 0;
            environmentTex.anisoLevel = 0;
            groundTex.anisoLevel = 0;

            // Releases unused memory
            Resources.UnloadUnusedAssets();
        }
    }
}


// lägg till ground i environmentlagret?
// varför fungerar inte shadern till gamet? rendererar inte ground eller?
// kolla FOW shader