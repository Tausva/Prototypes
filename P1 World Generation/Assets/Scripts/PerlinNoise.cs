using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100;
    public float offsetY = 100;

    public float bottom = 0;

    public List<TileHeight> tileHeights;

    private GameObject child;

    private void Start()
    {
        child = new GameObject("Tiles");
        child.transform.SetParent(transform);

        GenerateTexture();
    }

    private void GenerateTexture ()
    {
        //Generate a perlin noise map for texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                CalculateColor(x, y);
            }
        }
    }

    private void CalculateColor(int x, int y)
    {
        //Gets basic coordinates
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        //Gets the perlin noise
        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        //Adjust height
        float ofCenterX = x / (float)width * 2 - 1;
        float ofCenterY = y / (float)height * 2 - 1;
        sample -= Evaluate(Mathf.Max(Mathf.Abs(ofCenterX), Mathf.Abs(ofCenterY)));

        //HeightMap mapping
        float lastHeight = bottom;
        foreach (TileHeight tileHeight in tileHeights)
        {
            if (sample < tileHeight.height && sample > lastHeight)
            {
                GameObject obj = Instantiate(tileHeight.tile);
                obj.transform.position = new Vector2(x, y);
                obj.transform.SetParent(child.transform);
                lastHeight = sample;
            }
        }
    }

    private float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b -b * value, a));
    }
}

[System.Serializable]
public class TileHeight
{
    public GameObject tile;
    public float height;
}
