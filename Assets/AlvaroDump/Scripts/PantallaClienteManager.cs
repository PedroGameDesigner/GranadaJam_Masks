using System.Collections.Generic;
using UnityEngine;

public class PantallaClienteManager : MonoBehaviour
{
    public static PantallaClienteManager Instance;

    [SerializeField]
    GameObject comanda;

    [SerializeField]
    List<ClientScriptable> clients;
    List<GameObject> cliente;
    private bool justFirstTime = false;
    [SerializeField] private GameObject tutorialObject;

    private void Awake()
    {
        Instance = this;
    }

    public void LanzarComanda()
    {
        var client = clients[Random.Range(0, clients.Count)];
        GameObject comandatemp = Instantiate(comanda, transform);
        var orderDisplay = comandatemp.GetComponent<OrderDisplay>();
        GameObject clientetemp = Instantiate(client.clientPrefab, transform);
        orderDisplay.cliente = clientetemp;
        orderDisplay.clientScriptable = client;
        if (!justFirstTime)
        {
            justFirstTime = true;
            tutorialObject.SetActive(false);
        }
    }


}
