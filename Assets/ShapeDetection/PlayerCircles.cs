using System;
using UnityEngine;

public class PlayerCircles : MonoBehaviour
{
    [SerializeField] ShapeDetectionCircles closestCircle;
    [SerializeField] float distance;

    internal void SetClosestCircle(ShapeDetectionCircles closestCircle, float distance)
    {
        this.closestCircle = closestCircle;
        this.distance = distance;
    }

    private void OnDrawGizmos()
    {
        if (closestCircle != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, closestCircle.transform.position);
        }
    }
}
