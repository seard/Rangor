using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class EnvironmentSpawner : MonoBehaviour
{
    List<GameObject> list;
    Vector2 blockPos;

    public Texture2D blockMap;
    public Color[] colorCode;
    public GameObject[] prefabToColor;
    public int blocksPerUnit = 1;
    public int blocksPerPixel = 1;
    public float shiftY = 0.0f;

    public Vector2 mapSize;

    // Use this for initialization
    void Start()
    {
        if (colorCode.Length != prefabToColor.Length)
            Debug.Log("colorCode and prefabToColor must be same size");
        if (!blockMap)
            Debug.Log("Block map is not set");
        if (blockMap.width != blockMap.height)
            Debug.Log("Block map is not square");

        float[] shiftY = new float[colorCode.Length];

        for (int i = 0; i < colorCode.Length; i++)
        {
            colorCode[i].a = 1;
            // If prefabslot is not empty, set Y-size
            if (prefabToColor[i])
            {
                float spriteSizeX = prefabToColor[i].GetComponent<SpriteRenderer>().sprite.texture.width;
                float spriteSizeY = prefabToColor[i].GetComponent<SpriteRenderer>().sprite.texture.height;
                // Calculates shifted center Y-position, needed for placing rectangular blocks
                if (spriteSizeX != spriteSizeY)
                    shiftY[i] = (spriteSizeY - spriteSizeX) / (spriteSizeX * 2);
                else
                    shiftY[i] = 0;
            }
        }

        for (float i = 0; i < blockMap.width * blocksPerPixel; i++) // Iterate over width
        {
            for (float j = 0; j < blockMap.height * blocksPerPixel; j++) // Iterate over height
            {
                for (int k = 0; k < colorCode.Length; k++) // Iterate over all colorCodes
                {
                    Color pixelColor = blockMap.GetPixel((int)(i / blocksPerPixel), (int)(j / blocksPerPixel));
                    Color pixelColorRounded = new Color(Mathf.Round(pixelColor.r * 10.0f) / 10.0f, Mathf.Round(pixelColor.g * 10.0f) / 10.0f, Mathf.Round(pixelColor.b * 10.0f) / 10.0f);
                    if (pixelColorRounded == colorCode[k]) // If current pixel color equal to colorCode[k]
                    {
                        // If a prefab has been connected with colorCode
                        if (prefabToColor[k])
                        {
                            prefabToColor[k].transform.localScale = prefabToColor[k].transform.localScale / blocksPerUnit;

                            blockPos = new Vector2(i / blocksPerUnit, (j / blocksPerUnit * 0.65625f));
                            (Instantiate(prefabToColor[k], blockPos, prefabToColor[k].transform.rotation) as GameObject).transform.parent = transform;
                        }
                    }
                }
            }
        }

        mapSize = new Vector2(blockMap.width, blockMap.height * 0.65625f);
    }

    void OnApplicationQuit()
    {
        Destroy(this);
    }
}