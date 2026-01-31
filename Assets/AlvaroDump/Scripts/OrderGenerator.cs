using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField]
    List<Sprite> MaskShapes;
    [SerializeField]
    List<string> RandomRequest;
    public Order GenerateOrder()
    {
        Order order = new Order();
        order.maskShape = RandomMaskShape();
        order.colorRequirement = RandomColorRequirement();
        order.RandomRequirement = RandomRandomRequirement();
        return order;
    }

    private string RandomRandomRequirement()
    {
        return RandomRequest[UnityEngine.Random.Range(0, RandomRequest.Count)];
    }

    private ColorRequirement RandomColorRequirement()
    {
        System.Random random = new System.Random();
        ColorRequirement colorRequirement = new ColorRequirement();
        Array values = Enum.GetValues(typeof(MaskColor));
        colorRequirement.maskColor = (MaskColor)values.GetValue(random.Next(values.Length));
        colorRequirement.percentage = UnityEngine.Random.Range(0, 70);
        return colorRequirement;
    }

    private Sprite RandomMaskShape()
    {
        return MaskShapes[UnityEngine.Random.Range(0, MaskShapes.Count)];
    }
}
