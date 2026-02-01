using System.Collections.Generic;
using UnityEngine;

public class PantallaClienteManager : MonoBehaviour
{
    public static PantallaClienteManager Instance;

    [SerializeField]
    GameObject comanda;

    [SerializeField]
    List<GameObject> cliente;
    private bool justFirstTime = false;
    [SerializeField] private GameObject tutorialObject;

    private void Awake()
    {
        Instance = this;
    }

    public void LanzarComanda()
    {
        GameObject comandatemp = Instantiate(comanda, transform);
        GameObject clientetemp = Instantiate(cliente[Random.Range(0, cliente.Count)]);
        comandatemp.GetComponent<OrderDisplay>().cliente = clientetemp;
        if (!justFirstTime)
        {
            justFirstTime = true;
            tutorialObject.SetActive(false);
        }
    }


}
