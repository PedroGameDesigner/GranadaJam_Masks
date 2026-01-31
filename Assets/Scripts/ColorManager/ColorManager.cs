using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ColorManager : MonoBehaviour
{
  [SerializeField] private List<ColorConfig> colorList = new List<ColorConfig>();

   public string GetColorHex(string colorhex){

        foreach (var item in colorList)
        {
            if (item.colorHex == colorhex)
            {
                return item.colorHex;
            }
        }
        return null;
    }

    public MaskColor GetColorId(string colorhex) {

        foreach (var item in colorList)
        {
            if (item.colorHex == colorhex)
            {
                return item.colorId;
            }
        }
        return MaskColor.White;
    }
}
