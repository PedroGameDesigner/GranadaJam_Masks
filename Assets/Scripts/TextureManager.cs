using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public static TextureManager Instance { get; private set; }

    [SerializeField] private ColorConfig nullColor;
    [SerializeField] private Dictionary<int, Texture2D> tempTextures;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tempTextures = new Dictionary<int, Texture2D>();
    }
    public void SaveTexture(int id, Texture2D texture)
    {
        var copy = new Texture2D(texture.width, texture.height, texture.format, texture.mipmapCount, true);
        Graphics.CopyTexture(texture, copy);

        if (!tempTextures.ContainsKey(id))        
            tempTextures.Add(id, copy);        
        else
            tempTextures[id] = copy;
    }
    public Texture2D LoadTexture(int id)
    {
        return tempTextures[id];
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


        Debug.Log($"Check Color: {correctCount}/{coloredCount} ({correctCount * 100 / coloredCount}%, full={totalSize})");

        if (coloredCount/ totalSize > 0.1f)
        {
            return correctCount / coloredCount;
        }
        return 0;
    }

    public bool TextureExist(int id)
    {
        return tempTextures.ContainsKey(id);
    }
}
