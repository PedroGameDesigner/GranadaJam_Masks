using UnityEngine;
using UnityEngine.UI;

public class SetColorHelper : MonoBehaviour
{
    public void SetColor()
    {
        var color = GetComponent<Image>().color;

        FindFirstObjectByType<DrawManager>().SetColor(color);
    }
}
