using UnityEngine;


[CreateAssetMenu(fileName = "ColorConfig", menuName = "New Color Config")]
public class ColorConfig : ScriptableObject
{
    public string colorHex;
    public MaskColor colorId;
}
