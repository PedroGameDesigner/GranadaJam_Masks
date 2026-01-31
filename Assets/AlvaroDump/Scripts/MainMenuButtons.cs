using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{



    public void PlayButton()
    {
        MainMenuManager.Instance.GameStart();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    
}
