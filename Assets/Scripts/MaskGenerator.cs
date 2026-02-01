using System.Collections;
using UnityEngine;

public class MaskGenerator : MonoBehaviour
{
    [SerializeField] private EvaluationPanel evPanel;
    [SerializeField] private ShapePhaseController shapeController;
    [SerializeField] private PlayerInputDetection playerDetection;
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private DrawManager2D drawManager2D;
    [SerializeField] private GameObject Mask;
    [SerializeField] private float delayBeforeNewMask;
    private void OnEnable()
    {
        evPanel.OnEvaluationFinished += (f) => GetNewMask();
    }

    private void OnDisable()
    {
        evPanel.OnEvaluationFinished -= (f) => GetNewMask();
    }

    private void GetNewMask()
    {
        StartCoroutine(Delay(delayBeforeNewMask));
        
    }
    private IEnumerator Delay(float value)
    {
        yield return new WaitForSeconds(value);
        NewMask();
    }

    private void NewMask()
    {
        evPanel.HidePanel();
        drawManager2D.ResetCanvas();
        drawManager2D.HideSpriteRenderer();
        //shapeController.BeginShape();
        playerDetection.Reset();
        playerDetection.enableInput = false;
        Mask.transform.position = transform.localPosition +  new Vector3(100,0,0);
        PantallaClienteManager.Instance.LanzarComanda();
    }
}
