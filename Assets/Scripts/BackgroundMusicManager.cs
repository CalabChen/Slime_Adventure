using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager _instance;
    public static BackgroundMusicManager Instance { get { return _instance; } }

    [Tooltip("The AudioClip to play as background music.")]
    [SerializeField] private AudioClip backgroundMusicClip;

    private AudioSource audioSource;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Ensure the AudioSource component is available.
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        if (backgroundMusicClip == null)
        {
            Debug.LogWarning("No background music clip assigned. Please assign a valid AudioClip.");
        }
        else
        {
            // Initialize and play the background music.
            InitializeAudio();
        }
    }

    private void InitializeAudio()
    {
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f); // Load volume from player prefs or set default
        audioSource.Play();
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    /// <summary>
    /// Changes the volume of the background music.
    /// </summary>
    /// <param name="volume">A float value between 0 and 1 representing the new volume.</param>
    public void ChangeVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("BGMVolume", volume); // Save volume to player prefs
        }
    }

    /// <summary>
    /// Pauses the background music.
    /// </summary>
    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    /// <summary>
    /// Resumes playing the background music.
    /// </summary>
    public void ResumeMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    /// <summary>
    /// Stops the background music.
    /// </summary>
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Plays the background music.
    /// </summary>
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}