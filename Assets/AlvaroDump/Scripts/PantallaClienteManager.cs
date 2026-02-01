using System.Collections.Generic;
using UnityEngine;

public class PantallaClienteManager : MonoBehaviour
{
    public static PantallaClienteManager Instance;

    [SerializeField]
    GameObject comanda;

    [SerializeField]
    List<GameObject> cliente;

    private void Awake()
    {
        Instance = this;
    }

    public void LanzarComanda()
    {
        
        GameObject comandatemp = Instantiate(comanda, transform);
        GameObject clientetemp = Instantiate(cliente[Random.Range(0, cliente.Count)], transform);
        comandatemp.GetComponent<OrderDisplay>().cliente = clientetemp;
    }


}
