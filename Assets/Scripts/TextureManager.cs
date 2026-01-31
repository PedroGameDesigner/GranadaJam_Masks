using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class TextureManager : MonoBehaviour
{
    [SerializeField] private List<Texture2D> tempTextures;

    private void Start()
    {
        tempTextures = new List<Texture2D>();
    }
    public void SaveTexture(Texture2D texture)
    {
        tempTextures.Add(texture);
    }
}
