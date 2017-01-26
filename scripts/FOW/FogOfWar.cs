using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Everything with an observer attached will explore FOW
// Viewdistance dependent on Observer X-scale

[RequireComponent(typeof(SpriteRenderer))]

public class FogOfWar : MonoBehaviour {
	Texture2D textureFOW;
    Camera gameCam;
    GameObject[] observers;

	public Color darkFOW;
    public Color grayFOW;
    Color whiteFOW;
    public int resFOWX = 128;
    public int resFOWY = 128;
	public int blocksPerUnit = 2;
	public FilterMode filterFOW = FilterMode.Point;
    public float updateRate = 0.05f;
    public Vector3 shiftPos;

	void Start()
    {
        // Set default FOW color
        whiteFOW = new Color(0, 0, 0, 0);

        // Set the camera that renders the game
        gameCam = GameObject.FindWithTag("MainCamera").camera;

		// Initialize FOW texture values
		textureFOW = new Texture2D(resFOWX * blocksPerUnit, resFOWY * blocksPerUnit);
		textureFOW.filterMode = filterFOW;

		// Move FOW lower left corner to (0,0,?)
		//transform.position = new Vector3((resFOWX / 2), (resFOWY / 2), transform.position.z) + shiftPos;
        transform.position = Vector3.zero + shiftPos;
        transform.localScale = new Vector3(100, 100, 1) / blocksPerUnit;
		transform.rotation = new Quaternion(0, 0, 0, 0);

        // Set default black FOW		
		for(int i = 0; i < textureFOW.width; i++)
			for(int j = 0; j < textureFOW.height; j++)
                textureFOW.SetPixel(i, j, darkFOW);
        textureFOW.Apply();

        gameObject.layer = LayerMask.NameToLayer("FogOfWar");
        tag = "FogOfWar";
	}

	float deltaUpdateFOW = 0;
	void Update()
    {
        deltaUpdateFOW += Time.deltaTime;
        // Update FOW every <updateRate> miliseconds
        if(deltaUpdateFOW >= updateRate)
        {
            observers = GameObject.FindGameObjectsWithTag("Observer");
            deltaUpdateFOW = 0;
            UpdateFOW();
        }
	}

	void UpdateFOW()
    {        
        Vector2 viewportToScreenAndFOWStart = gameCam.ViewportToWorldPoint(Vector3.zero) - transform.position;
        Vector2 viewportToScreenAndFOWEnd = gameCam.ViewportToWorldPoint(Vector3.one) - transform.position;

        int iterationStartX = Mathf.FloorToInt(viewportToScreenAndFOWStart.x) - 1;
        int iterationCompareX = Mathf.CeilToInt(viewportToScreenAndFOWEnd.x) + 1;
        int iterationStartY = Mathf.FloorToInt(viewportToScreenAndFOWStart.y) - 1;
        int iterationCompareY = Mathf.CeilToInt(viewportToScreenAndFOWEnd.y) + 1;
        
        // Iterate all pixels in camera view
        for (int i = iterationStartX; i < iterationCompareX; i++)
        {
            for (int j = iterationStartY; j < iterationCompareY; j++)
            {
                // If not outside of FOW-array
                if (i >= 0 && i <= resFOWX * blocksPerUnit && j >= 0 && j <= resFOWY * blocksPerUnit)
                {
                    // Read existing FOW texture
                    Color shade = textureFOW.GetPixel(i, j);
                    bool visible = false;
                    // Iterate all observers in the list
                    foreach(GameObject o in observers)
                    {
                        // If inside object circle, set visibility or pixel to true
                        float viewRange = o.transform.localScale.x * blocksPerUnit;
                        // Calculating observers position on FOW to pick correct FOW pixels to edit
                        // Subtracting [0.5, 0.5, 0.5] to center observer to FOW
                        Vector2 observerToFOW = (o.transform.position - transform.position) * blocksPerUnit - Vector3.one * 0.5f;
                        if (Vector2.Distance(observerToFOW, new Vector2(i, j)) <= viewRange)
                        {
                            shade = whiteFOW;
                            visible = true;
                        }
                        else if (shade == whiteFOW && !visible)
                            shade = grayFOW;
                    }
                    textureFOW.SetPixel(i, j, shade);
                }
            }
        }
        // Apply texture pixel changes
		textureFOW.Apply();
		
        // Update sprite texture
		GetComponent<SpriteRenderer>().sprite = Sprite.Create(
			textureFOW,
            new Rect(0, 0, resFOWX * blocksPerUnit, resFOWY * blocksPerUnit), 
			new Vector2(0.0f, 0.0f));
        GetComponent<SpriteRenderer>().

        // Update shader texture
		GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", textureFOW);
	}
}
