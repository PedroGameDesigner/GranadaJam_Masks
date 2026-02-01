using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] musica;
    public static MusicManager Instance;
    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        PlayMenuMusic();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = musica[Random.Range(0,2)];
        musicSource.Play();
    }
}
