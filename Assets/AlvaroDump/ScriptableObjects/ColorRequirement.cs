using UnityEngine;

public class ColorRequirement
{
    public int percentage;
    public ColorConfig maskColor;        

    public string createMesageRequirement()
    {
        string mesagge = "";

        if (percentage > 70)
        {
            mesagge = "Quiero que sea muy "+ maskColor.colorName;
        } else if (percentage > 40)
        {
            mesagge = "Quiero que sea " + maskColor.colorName;
        } else if (percentage > 15)
        {
            mesagge = "Quiero que sea un poco " + maskColor.colorName;
        } else {
            mesagge = "Hazla del color que quieras";
        }

        return mesagge;
    }
}
