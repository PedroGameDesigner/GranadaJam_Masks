using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    
    public static MainMenuManager Instance;

    [SerializeField] 
    Animator animatorMainMenu;


    private void Awake()
    {
        Instance = this;
    }

    public void GameStart()
    {
        //AnimacionMainMenu
        animatorMainMenu.SetTrigger("play");
        TutorialBehavior.Instance.AparicionTutorial();

    }

}
