using System;
using UnityEngine;

public class PlayerCircles : MonoBehaviour
{
    [SerializeField] ShapeDetectionCircles closestCircle;
    [SerializeField] float distance;

    bool inPerfectDistance;

    internal void SetClosestCircle(ShapeDetectionCircles closestCircle, float distance, float perfectDistance)
    {
        this.closestCircle = closestCircle;
        this.distance = distance;
        inPerfectDistance = distance<perfectDistance;
    }

    public float GetScore()
    {
        if (closestCircle == null)
            return 0;
        else if (inPerfectDistance)
            return 1;
        else
            return 0.75f;
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
