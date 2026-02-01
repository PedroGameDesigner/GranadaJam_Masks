using System.Collections.Generic;
using UnityEngine;

public class PantallaClienteManager : MonoBehaviour
{
    public static PantallaClienteManager Instance;

    [SerializeField]
    GameObject comanda;

    [SerializeField]
    List<ClientScriptable> clients;

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
    }


}
