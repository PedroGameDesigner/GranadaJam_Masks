using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDetection : MonoBehaviour
{
    [SerializeField] PlayerCircles circles;
    [SerializeField] float placementDistance;
    [Space]
    [SerializeField] Vector2 screenCoords;
    [SerializeField] Vector3 worldPoint;

    bool pathStarted;
    bool pathCompleted;
    Vector3 lastPoint;
    List<PlayerCircles> spawnedCircles = new List<PlayerCircles>();

    // Update is called once per frame
    void Update()
    {
        if (pathCompleted) return;

        var mousePressed = Mouse.current.leftButton.IsPressed();
        if (mousePressed)
        {
            screenCoords = Mouse.current.position.value;
            worldPoint = Camera.main.ScreenToWorldPoint(screenCoords);
            worldPoint.z = 0;
            
            if (!pathStarted)
            {
                GeneratePoints();
                pathStarted = true;
            }
            else if ((worldPoint - lastPoint).magnitude > placementDistance)
            {
                GeneratePoints();
                pathStarted = true;
            }
        }

        if (!mousePressed && pathStarted)
        {
            pathCompleted = true;
            CheckPath();
        }
    }

    private void CheckPath()
    {
        for (int i = 0; i < spawnedCircles.Count; i++)
        {
            ShapeDetectionCircles closestCircle = null;
            float distance = Mathf.Infinity;
            for (int j = 0; j < ShapeDetectionCircles.list.Count; j++)
            {
                var shapeCircle = ShapeDetectionCircles.list[j];
                var shapeDistance = (shapeCircle.transform.position - spawnedCircles[i].transform.position).magnitude;
                if (closestCircle == null || shapeDistance < distance)
                {
                    closestCircle = shapeCircle;
                    distance = shapeDistance;
                }
            }

            spawnedCircles[i].SetClosestCircle(closestCircle, distance);
            closestCircle.SetClosestCircle(spawnedCircles[i], distance);
        }
    }

    private void GeneratePoints()
    {
        var circle = Instantiate(circles, worldPoint, Quaternion.identity, transform);
        spawnedCircles.Add(circle);
        lastPoint = worldPoint;
    }
}
