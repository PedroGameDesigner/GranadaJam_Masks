using UnityEngine;
using UnityEngine.Events;

public class PlayOnStart : MonoBehaviour
{
    public UnityEvent action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action.Invoke();
    }
}
