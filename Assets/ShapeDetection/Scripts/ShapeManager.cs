using UnityEngine;
using System.Collections.Generic;
using System;

public class ShapeManager : MonoBehaviour
{
    public static ShapeManager Instance { get; private set; }

    Dictionary<int, Vector2[]> shapeDictionary = new Dictionary<int, Vector2[]>();

    private void Awake()
    {
        Instance = this;
    }

    public void SaveShape(int id, Vector2[] vertices)
    {
        if (!shapeDictionary.ContainsKey(id))
            shapeDictionary.Add(id, vertices);
        else
            shapeDictionary[id] = vertices;
    }

    public Vector2[] GetShape(int id)
    {
        if (shapeDictionary.ContainsKey(id))
            return shapeDictionary[id];
        else
            return new Vector2[0];
    }

    public bool ShapeExist(int id)
    {
        return shapeDictionary.ContainsKey(id);
    }
}
