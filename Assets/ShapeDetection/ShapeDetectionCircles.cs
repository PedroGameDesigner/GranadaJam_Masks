using System;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDetectionCircles : MonoBehaviour
{
    public static List<ShapeDetectionCircles> list = new List<ShapeDetectionCircles> ();

    private SpriteRenderer sprite;
    private Color asignedColor = Color.blue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        list.Add(this);
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        list.Remove(this);
    }

    internal void SetClosestCircle(PlayerCircles playerCircles, float distance)
    {
        var alpha = sprite.color.a;
        var color = asignedColor;
        color.a = alpha;
        sprite.color = color;
    }
}
