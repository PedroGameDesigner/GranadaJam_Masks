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

    private void Awake()
    {
        Instance = this;
    }

    public void LanzarComanda()
    {
        CurrentClient = clients[Random.Range(0, clients.Count)];

        GameObject comandatemp = Instantiate(comanda, transform);
        var orderDisplay = comandatemp.GetComponent<OrderDisplay>();

        GameObject clientetemp = Instantiate(CurrentClient.clientPrefab);
        clientetemp.GetComponent<WearMask>().PutOnMask(CurrentClient.clientId);
        orderDisplay.cliente = clientetemp;
        orderDisplay.clientScriptable = CurrentClient;

        if (!justFirstTime)
        {
            justFirstTime = true;
            tutorialObject.SetActive(false);
        }
    }
}
