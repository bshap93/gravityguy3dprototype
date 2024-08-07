using UnityEngine;
using UnityEngine.UI;

public class GridTextureGenerator : MonoBehaviour
{
    public int textureSize = 512;
    public int gridSize = 64;
    public Color backgroundColor = Color.black;
    public Color lineColor = Color.white;
    public int lineThickness = 1;

    void Start()
    {
        Texture2D gridTexture = GenerateGridTexture();
        GetComponent<RawImage>().texture = gridTexture;
    }

    Texture2D GenerateGridTexture()
    {
        Texture2D texture = new Texture2D(textureSize, textureSize);

        // Fill the texture with the background color
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                texture.SetPixel(x, y, backgroundColor);
            }
        }

        // Draw the grid lines
        for (int y = 0; y < textureSize; y += gridSize)
        {
            for (int t = 0; t < lineThickness; t++)
            {
                if (y + t < textureSize)
                {
                    for (int x = 0; x < textureSize; x++)
                    {
                        texture.SetPixel(x, y + t, lineColor);
                    }
                }
            }
        }

        for (int x = 0; x < textureSize; x += gridSize)
        {
            for (int t = 0; t < lineThickness; t++)
            {
                if (x + t < textureSize)
                {
                    for (int y = 0; y < textureSize; y++)
                    {
                        texture.SetPixel(x + t, y, lineColor);
                    }
                }
            }
        }

        texture.Apply();
        return texture;
    }
}
