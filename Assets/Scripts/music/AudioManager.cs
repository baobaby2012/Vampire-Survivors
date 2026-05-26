using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("---- Audio Sources ----")]
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource;  

    [Header("---- Background Music ----")]
    public AudioClip ingameBGM;

    [Header("---- Audio Clips Vũ Khí ----")]
    public AudioClip whipSFX;
    public AudioClip lightningSFX;
    public AudioClip fireballSFX;
    public AudioClip axeSFX;

    [Header("---- Audio Clips Hệ Thống ----")]
    public AudioClip levelUpSFX;
    public AudioClip afterUpgradeSelectSFX;

    public static AudioManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(ingameBGM);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true; 
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}