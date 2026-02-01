using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public static MaskManager Instance { get; private set; }

    public Vector3 maskStoragePosition;

    Dictionary<int, GameObject> maskDictionary = new Dictionary<int, GameObject>();
    Dictionary<int, MaterialPropertyBlock> mpbs = new Dictionary<int, MaterialPropertyBlock>();

    private void Awake()
    {
        Instance = this;
    }

    public void SaveMask(int id, GameObject mask, Texture2D texture)
    {
        var copiedMask = Instantiate(mask);
        copiedMask.transform.position = maskStoragePosition;
        copiedMask.name = copiedMask.name + "_COPY"+id;

        ModifySprite(id, copiedMask.GetComponentInChildren<SpriteRenderer>(), texture);

        if (!maskDictionary.ContainsKey(id))
            maskDictionary.Add(id, copiedMask);
        else
            maskDictionary[id] = copiedMask;
    }

    public GameObject LoadMask(int id)
    {
        if (maskDictionary.ContainsKey(id))
            return maskDictionary[id];
        else
            return null;
    }

    public void DeleteMask(int id)
    {
        if (maskDictionary.ContainsKey(id))
        {
            var mask = maskDictionary[id];
            maskDictionary.Remove(id);
            mpbs.Remove(id);
            Destroy(mask);
        }
    }

    private void ModifySprite(int id, SpriteRenderer renderer, Texture2D texture)
    {
        if (!mpbs.ContainsKey(id))
            mpbs.Add(id, new MaterialPropertyBlock());
        
        renderer.GetPropertyBlock(mpbs[id]);
        mpbs[id].SetTexture("_MainTex", texture);
        renderer.SetPropertyBlock(mpbs[id]);
    }
}
