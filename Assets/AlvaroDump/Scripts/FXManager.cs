using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance;

    [SerializeField] AudioSource m_AudioSource;


    private void Awake()
    {
        Instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.PlayOneShot(audio);
        }
    }
}
