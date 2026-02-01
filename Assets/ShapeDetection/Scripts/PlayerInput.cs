using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInputDetection : MonoBehaviour
{
    [SerializeField] PlayerCircles circles;
    [SerializeField] float placementDistance;

    [Header("Evaluation")]
    [SerializeField] float maxAssignDistance;
    [SerializeField] float perfectDistance;
    [SerializeField] UnityEvent completeEvaluation;


    [Header("Debug values")]
    [SerializeField] Vector2 screenCoords;
    [SerializeField] Vector3 worldPoint;

    bool pathStarted;
    bool pathCompleted;
    Vector3 lastPoint;
    List<PlayerCircles> spawnedCircles = new List<PlayerCircles>();

    public float ShapeScore { get; internal set; }
    public System.Action OnMaskCreated;

    private Coroutine drawCroroutine;
    [SerializeField] private float delayTime = 3f;
    bool delayCompleted = false;
    public bool enableInput =false;

    // Update is called once per frame
    void Update()
    {
        if(!enableInput) return;

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

    public void CheckPath()
    {
        for (int i = 0; i < spawnedCircles.Count; i++)
        {
            ShapeDetectionCircles closestCircle = null;
            float distance = maxAssignDistance;
            for (int j = 0; j < ShapeDetectionCircles.list.Count; j++)
            {
                var shapeCircle = ShapeDetectionCircles.list[j];
                var shapeDistance = (shapeCircle.transform.position - spawnedCircles[i].transform.position).magnitude;
                if (shapeDistance < distance)
                {
                    closestCircle = shapeCircle;
                    distance = shapeDistance;
                }
            }

            spawnedCircles[i].SetClosestCircle(closestCircle, distance, perfectDistance);
            if (closestCircle != null)
                closestCircle.SetClosestCircle(spawnedCircles[i], distance, perfectDistance);
        }

        ShapeScore = 0;
        float totalCircles = ShapeDetectionCircles.list.Count;
        for (int i = 0; i < ShapeDetectionCircles.list.Count; i++)
        {
            var shapeCircle = ShapeDetectionCircles.list[i];
            ShapeScore += shapeCircle.GetScore();
        }
        ShapeScore /= ShapeDetectionCircles.list.Count;
        completeEvaluation?.Invoke();
        OnMaskCreated?.Invoke();
    }

    private void GeneratePoints()
    {
        var circle = Instantiate(circles, worldPoint, Quaternion.identity, transform);
        spawnedCircles.Add(circle);
        lastPoint = worldPoint;
    }

    public List<Vector2> GetPointList()
    {
        List<Vector2> points = new List<Vector2>();
        Vector3 previousPoint = Vector2.one * Mathf.Infinity;
        for (int i = 0; i < spawnedCircles.Count; i++)
        {
            if ((spawnedCircles[i].transform.position - previousPoint).magnitude > maxAssignDistance)
            {
                previousPoint = spawnedCircles[i].transform.position;
                points.Add(spawnedCircles[i].transform.position);
            }
            else
                Debug.Log($"{i}: {(spawnedCircles[i].transform.position - previousPoint).magnitude}");
        }
        return points;
    }

    internal void HidePoints()
    {
        for (int i = 0; i < spawnedCircles.Count; i++)
        {
            spawnedCircles[i].gameObject.SetActive(false);
        }
    }

    internal void Reset()
    {
        for (int i = 0; i < spawnedCircles.Count; i++)
            Destroy(spawnedCircles[i].gameObject);
        spawnedCircles.Clear();

        pathStarted = false;
        pathCompleted = false;
    }
}
