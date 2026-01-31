using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float[] moneyAmount;
    [SerializeField] private EvaluationPanel evPanel;
    private float currentMoney;

    private void OnEnable()
    {
        evPanel.OnEvaluationFinished += (score) => GiveMoney(score);
    }

    private void OnDisable()
    {
        evPanel.OnEvaluationFinished -= (score) => GiveMoney(score);

    }

    private void GiveMoney(float score)
    {
        if(score <= 0.2f)
        {
            AddMoney(moneyAmount[0]);
        }
        if(score> 0.2f && score < 0.5f)
        {
            AddMoney(moneyAmount[1]);
        }
        if (score >= 0.5f)
        {
            AddMoney(moneyAmount[2]);
        }

    }

    private void AddMoney(float amount)
    {
        currentMoney += amount;
        moneyText.text = currentMoney.ToString();

    }

    private bool SpendMoney(float amount)
    {
        if (currentMoney < amount) { return false; }
        else
        {
            currentMoney -= amount;
            moneyText.text = currentMoney.ToString();
            return true;
        }
    }

    
}
