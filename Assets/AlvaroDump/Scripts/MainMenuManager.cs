using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    
    public static MainMenuManager Instance;

    [SerializeField] 
    Animator animatorMainMenu;

    public UnityEvent activateFirstStep;

    private void Awake()
    {
        Instance = this;
    }

    public void GameStart()
    {
        //AnimacionMainMenu
        animatorMainMenu.SetTrigger("play");
        activateFirstStep.Invoke();
    }
}
