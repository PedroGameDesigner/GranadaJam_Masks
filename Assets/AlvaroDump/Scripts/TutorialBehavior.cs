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

    public void AparicionTutorial()
    {
        animator.SetTrigger("appear");
    }

    private void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        TerminarTutorial();
    }

    public void TerminarTutorial()
    {
        if (!tutorialTerminado) 
        {
            tutorialTerminado = true;
            PantallaClienteManager.Instance.LanzarComanda();
        }
    }

    public void TutorialActivarClick()
    {
        tutorialTerminado = false;
    }
}
