using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class EvaluationPanel : MonoBehaviour
{
    [Header("Panel Elements")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image fillBar;
    [SerializeField] TextMeshProUGUI fillBarText;
    [SerializeField] Gradient fillBarGradient;
    [SerializeField] Image colorCheck;
    [SerializeField] Image conditionCheck;

    [Header("Config")]
    [SerializeField] float maxScore;
    [SerializeField] float scoreToFit;
    [SerializeField] float checkReduction;
    [SerializeField] float fadeInTime;
    [SerializeField] float fillTime;
    [SerializeField] float betweenChecksTime;

    [Header("References")]
    [SerializeField] TextureManager textureManager;
    [SerializeField] ShapePhaseController shapePhaseController;

    float rawScore;
    float convertedScore;
    bool hasColor;
    bool hasFit;

    public Action<float> OnEvaluationFinished;

    public void StartEvaluation()
    {
        var order = PantallaClienteManager.Instance.CurrentComanda.order;
        conditionCheck.gameObject.SetActive(false);
        colorCheck.gameObject.SetActive(false);

        rawScore = shapePhaseController.ShapeScore;
        convertedScore = rawScore / maxScore;
        hasFit = convertedScore > scoreToFit;
        if (hasFit)
            convertedScore -= checkReduction;
        hasColor = textureManager.CheckColor(0, order.colorRequirement.maskColor) > order.colorRequirement.percentage;
        if (hasColor)
            convertedScore -= checkReduction;

        StartCoroutine(EvaluationAnimation());
    }

    public IEnumerator EvaluationAnimation()
    {
        UpdateFillBar(0);

        //Fade In
        float time = 0;
        while (time < fadeInTime)
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0, fadeInTime);
            var timeNormalized = time / fadeInTime;
            canvasGroup.alpha = timeNormalized;
            yield return null;
        }

        //Shape Evaluation
        time = 0;
        while (time < fillTime)
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0, fillTime);
            var timeNormalized = time / fillTime;
            var score = convertedScore * timeNormalized;
            UpdateFillBar(score);

            yield return null;
        }

        yield return new WaitForSeconds(betweenChecksTime);
        //Check Color
        if (hasColor)
        {
            colorCheck.gameObject.SetActive(true);
            time = 0;
            var scoreStart = convertedScore;
            var scoreEnd = convertedScore + checkReduction;
            while (time < betweenChecksTime)
            {
                time = Mathf.Clamp(time + Time.deltaTime, 0, betweenChecksTime);
                var timeNormalized = time / betweenChecksTime;
                var score = Mathf.Lerp(scoreStart, scoreEnd, timeNormalized);
                UpdateFillBar(score);
                yield return null;
            }
            convertedScore = scoreEnd;
        }
        //Check Color
        if (hasFit)
        {
            conditionCheck.gameObject.SetActive(true);
            time = 0;
            var scoreStart = convertedScore;
            var scoreEnd = convertedScore + checkReduction;
            while (time < betweenChecksTime)
            {
                time = Mathf.Clamp(time + Time.deltaTime, 0, betweenChecksTime);
                var timeNormalized = time / betweenChecksTime;
                var score = Mathf.Lerp(scoreStart, scoreEnd, timeNormalized);
                UpdateFillBar(score);
                yield return null;
            }
            convertedScore = scoreEnd;
            
        }
        OnEvaluationFinished?.Invoke(convertedScore);
    }

    void UpdateFillBar(float score)
    {
        var overTenScore = $"{(score * 10f):N2}";
        var fillColor = fillBarGradient.Evaluate(score);

        fillBar.fillAmount = score;
        fillBar.color = fillColor;
        fillBarText.text = $"{overTenScore}/10";
    }

    public void HidePanel()
    {
        canvasGroup.alpha = 0f;
    }
}
