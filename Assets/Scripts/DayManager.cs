using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private int currentDay = 1;

    private void GoToNextDay()
    {
        currentDay++;
        dayText.text = currentDay.ToString();
    }

}
