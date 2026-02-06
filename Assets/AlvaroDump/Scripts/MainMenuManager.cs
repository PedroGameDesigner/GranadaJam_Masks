using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    
    public static MainMenuManager Instance;
    public GameObject nosotros;
    public Animator animacionPersonajesInicio;
    public AudioClip clickSound;
    public AudioClip hoverSound;
    public float hoverVolume = 1f;
    public Sprite hoversprite;


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
        FXManager.Instance.PlaySound(clickSound);
        nosotros.SetActive(false);
        animacionPersonajesInicio.SetTrigger("hide");
    }

    public void HoverButton()
    {
        FXManager.Instance.PlaySound(hoverSound, hoverVolume);
    }

    public void HoverButtonDisplay(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetBool("hover", true);
    }

    public void HoverButtonHide(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetBool("hover", false);
    }

    public void ClickButton()
    {
        FXManager.Instance.PlaySound(clickSound);
    }

}
