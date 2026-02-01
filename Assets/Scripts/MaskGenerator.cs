using UnityEngine;

public class MaskGenerator : MonoBehaviour
{
    [SerializeField] private EvaluationPanel evPanel;
    [SerializeField] private ShapePhaseController shapeController;
    [SerializeField] private PlayerInputDetection playerDetection;
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private ClientScriptable clientsScriptable;
    private void OnEnable()
    {
        evPanel.OnEvaluationFinished += (f) => GetNewMask();
    }

    private void OnDisable()
    {
        evPanel.OnEvaluationFinished += (f) => GetNewMask();
    }

    private void GetNewMask()
    {

        shapeController.BeginShape(clientsScriptable);
        playerDetection.Reset();
        

    }

}
