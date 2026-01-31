using UnityEngine;


[CreateAssetMenu(fileName = "ColorConfig", menuName = "New Color Config")]
public class ColorConfig : ScriptableObject
{
    public string colorHex;
    public MaskColor colorId;

    private Color color;
    private void OnEnable()
    {
        ColorUtility.TryParseHtmlString(colorHex, out color);
    }

    public bool IsColor(Color color)
    {
        return this.color == color;
    }
}
