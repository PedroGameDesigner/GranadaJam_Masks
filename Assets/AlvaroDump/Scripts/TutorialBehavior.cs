using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialBehavior : MonoBehaviour
{

    public static TutorialBehavior Instance;
    Animator animator;
    bool tutorialTerminado = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Mouse.current.leftButton.IsActuated())
        {
            TerminarTutorial();
        }
    }


    public void AparicionTutorial()
    {
        animator.SetTrigger("appear");
    }

    public void TutorialPuedeTerminar()
    {
        tutorialTerminado = false;
    }


    public void TerminarTutorial()
    {
        if (!tutorialTerminado) 
        {
            Debug.Log("ComandaLanzada");
            tutorialTerminado = true;
            PantallaClienteManager.Instance.LanzarComanda();
        }
    }

    public void TutorialActivarClick()
    {
        tutorialTerminado = false;
    }
}
