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

    [SerializeField]
    private List<ColorHeight> colorHeights;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture ()
    {
        Texture2D texture = new Texture2D(width, height);

        //Generate a perlin noise map for texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y)
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
        foreach (ColorHeight colorHeight in colorHeights)
        {
            if (sample < colorHeight.height)
            {
                return colorHeight.color;
            }
        }
        return new Color();
    }

    private float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b -b * value, a));
    }
}

[System.Serializable]
public class ColorHeight
{
    public Color color;
    public float height;
}
