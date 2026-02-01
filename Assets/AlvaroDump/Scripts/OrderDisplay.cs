using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    [SerializeField]
    Image silueta;

    [SerializeField]
    TextMeshProUGUI colorRequirementTMP;

    [SerializeField]
    TextMeshProUGUI randomRequirementTMP;

    [SerializeField]
    public GameObject cliente;

    OrderGenerator orderGenerator;
    Order order;
    bool colorTyped;

    [SerializeField]
    float timeBetweenCharacters;

    Coroutine typingCoroutine;

    private void Awake()
    {
        orderGenerator = GetComponent<OrderGenerator>();
    }

    public void showOrder()
    {
        order = orderGenerator.GenerateOrder();
        silueta.gameObject.SetActive(true);
        colorRequirementTMP.gameObject.SetActive(true); 
        randomRequirementTMP.gameObject.SetActive(true);
        StartTyping(order.colorRequirement.createMesageRequirement(), colorRequirementTMP, order.RandomRequirement, randomRequirementTMP);
    
    }


    private void StartTyping(string text, TextMeshProUGUI textUI, string text2, TextMeshProUGUI textUI2)
    {
        // Si ya estaba escribiendo, lo detenemos
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        StartCoroutine(TypeText(text, textUI, text2, textUI2));
    }

    private IEnumerator TypeText(string text, TextMeshProUGUI textUI, string text2, TextMeshProUGUI textUI2)
    {
        textUI.text = "";

        foreach (char c in text)
        {
            textUI.text += c;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        textUI2.text = "";

        foreach (char c in text2)
        {
            textUI2.text += c;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        typingCoroutine = null;
        cliente.GetComponent<Animator>().SetTrigger("disapear");

        yield return new WaitForSeconds(1f);
        ShapePhaseController.Instance.BeginShape();

        ShapePhaseController.Instance.gameObject.GetComponentInChildren<PlayerInputDetection>().enabled = true;


    }

    
}
