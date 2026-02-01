using UnityEngine;

public class TutorialBehavior : MonoBehaviour
{

    public static TutorialBehavior Instance;
    Animator animator;
    bool tutorialTerminado = false;

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

    public void TerminarTutorial()
    {
        Debug.Log("ComandaLanzada");
        if (!tutorialTerminado) 
        {
            Debug.Log("ComandaLanzada");
            tutorialTerminado = false;
            PantallaClienteManager.Instance.LanzarComanda();
        }
    }

    public void TutorialActivarClick()
    {
        tutorialTerminado = false;
    }
}
