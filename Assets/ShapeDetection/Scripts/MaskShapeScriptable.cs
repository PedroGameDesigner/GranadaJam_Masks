using UnityEngine;
using UnityEngine.Splines;

[CreateAssetMenu(fileName = "Mask Shape", menuName = "MaskShape", order = 1)]
public class MaskShapeScriptable : ScriptableObject
{
    public SplineContainer shapeSplinePrefab;
    public Sprite icon;
}
