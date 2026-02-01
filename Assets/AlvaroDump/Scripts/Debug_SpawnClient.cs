using UnityEngine;

public class Debug_SpawnClient : MonoBehaviour
{
    public ClientScriptable client;

    public void spawn()
    {
        Instantiate(client.clientPrefab, transform);

    }
}
