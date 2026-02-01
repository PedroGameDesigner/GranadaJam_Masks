using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    [SerializeField] private ColorConfig nullColor;
    [SerializeField] private List<Texture2D> tempTextures;

    private void Start()
    {
        tempTextures = new List<Texture2D>();
    }
    public void SaveTexture(Texture2D texture)
    {
        tempTextures.Add(texture);
    }

    public float CheckColor(int textureId, ColorConfig color)
    {
        var texture = tempTextures[textureId];
        int totalSize = texture.width * texture.height;
        float coloredCount = 0;
        float correctCount = 0;

        for (int i = 0; i < texture.width; i++)
            for (int j = 0; j < texture.height; j++)
            {
                var pixel = texture.GetPixel(i, j);
                if (!nullColor.IsColor(pixel))
                    coloredCount++;
                if (color.IsColor(pixel))
                    correctCount++;
            }


        Debug.Log($"{correctCount}/{coloredCount} ({correctCount * 100 / coloredCount}%, full={totalSize})");

        if (coloredCount/ totalSize > 0.1f)
        {
            return correctCount / coloredCount;
        }
        return 0;
    }
}
