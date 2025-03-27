using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance { get; private set; }
    public AudioClip flap;
    public AudioClip music;
    public AudioClip waves;
    public AudioClip pop;
    public AudioClip splat;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Title")
        {
            audioSource.clip = waves;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(flap);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayGameMusic()
    {
        if (waves != null && audioSource != null)
        {
            audioSource.clip = music;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }
}