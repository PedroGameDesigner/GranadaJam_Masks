using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "Client")]
public class ClientScriptable : ScriptableObject
{
    public int clientId;
    public GameObject clientPrefab;
}
