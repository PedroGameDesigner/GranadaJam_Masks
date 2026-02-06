using System.Collections.Generic;
using UnityEngine;

public class PantallaClienteManager : MonoBehaviour
{
    public static PantallaClienteManager Instance;

    [SerializeField]
    GameObject comanda;

    [SerializeField]
    List<ClientScriptable> clients;

    private bool justFirstTime = false;
    [SerializeField] private GameObject tutorialObject;

    public ClientScriptable CurrentClient { get; private set; }
    public OrderDisplay CurrentComanda { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LanzarComanda()
    {
        CurrentClient = clients[Random.Range(0, clients.Count)];

        GameObject comandatemp = Instantiate(comanda, transform);
        CurrentComanda = comandatemp.GetComponent<OrderDisplay>();

        GameObject clientetemp = Instantiate(CurrentClient.clientPrefab);
        clientetemp.GetComponent<WearMask>().PutOnMask(CurrentClient.clientId);
        CurrentComanda.cliente = clientetemp;
        CurrentComanda.clientScriptable = CurrentClient;

        if (!justFirstTime)
        {
            justFirstTime = true;
            //tutorialObject.SetActive(false);
        }
    }

    public void HideComanda()
    {
        CurrentComanda.gameObject.GetComponent<Animator>().SetTrigger("hide");
    }
}
