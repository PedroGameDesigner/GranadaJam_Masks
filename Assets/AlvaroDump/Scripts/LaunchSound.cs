using UnityEngine;

public class LaunchSound : MonoBehaviour
{
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip hablarPersonaje;

    public void LaunchPaperSound()
    {
        FXManager.Instance.PlaySound(paperSound);
    }
    public void LaunchPersonajeHablando()
    {
        FXManager.Instance.PlaySound(hablarPersonaje);
    }
}
