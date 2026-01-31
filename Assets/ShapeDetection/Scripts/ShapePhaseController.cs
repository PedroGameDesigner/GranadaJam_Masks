using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

public class ShapePhaseController : MonoBehaviour
{
    [SerializeField] private PlayerInputDetection input;
    [SerializeField] private SpriteShapeController shape;
    [SerializeField] private SplineContainer spline;
    [SerializeField] private PolygonCollider2D polygonCollider;

    public void ShapeCompleted()
    {
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
    }
}
