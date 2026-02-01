using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.Events;

public class ShapePhaseController : MonoBehaviour
{
    public static ShapePhaseController Instance;

    [SerializeField] private PlayerInputDetection input;
    [SerializeField] private SpriteShapeController shape;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [Header("Shapes")]
    [SerializeField] private MaskShapeScriptable[] maskShapes;
    [Header("Events")]
    [SerializeField] private UnityEvent phaseCompletedEvent;

    private SplineContainer spline;

    public float ShapeScore { get; internal set; }
    public MaskShapeScriptable selectedMask { get; internal set; }

    public void BeginShape()
    {
        selectedMask = maskShapes[Random.Range(0, maskShapes.Length)];
        spline = Instantiate(selectedMask.shapeSplinePrefab, transform);
        input.Reset();
    }

    public void ShapeCompleted()
    {
        ShapeScore = input.ShapeScore;
        var pointList = input.GetPointList();
        input.HidePoints();
        spline.gameObject.SetActive(false);
        shape.gameObject.SetActive( true);
        shape.spline.Clear();
        for (int i = 0; i < pointList.Count; i++)
        {
            shape.spline.InsertPointAt(i, pointList[i]);
        }

        for (int i = 0; i < pointList.Count; i++)
        {
            polygonCollider.points = pointList.ToArray();
        }
        phaseCompletedEvent?.Invoke();
    }

    private void Awake()
    {
        Instance = this;
    }

}
