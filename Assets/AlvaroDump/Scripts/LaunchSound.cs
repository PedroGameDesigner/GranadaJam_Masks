using UnityEngine;

public class LaunchSound : MonoBehaviour
{
    [SerializeField] private AudioClip paperSound;

    public void LaunchPaperSound()
    {
        FXManager.Instance.PlaySound(paperSound);
    }
}
