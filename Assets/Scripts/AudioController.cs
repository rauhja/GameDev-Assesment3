using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioController : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    private AudioSource _musicAudioSource;
    
    // Start is called before the first frame update
    private void Start()
    {
        _musicAudioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMenuMusic();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                PlayMenuMusic();
                break;
            case 1:
                PlayIntroMusic();
                break;
        }
    }
    void PlayMenuMusic()
    {
        _musicAudioSource.clip = backgroundMusic[0];
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    void PlayIntroMusic()
    {
        _musicAudioSource.clip = backgroundMusic[1];
        _musicAudioSource.Play();
        
        Invoke(nameof(PlayMainLoop), backgroundMusic[1].length);
    }

    void PlayMainLoop()
    {
        _musicAudioSource.clip = backgroundMusic[2];
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    public void PlayGhostDownLoop()
    {
        _musicAudioSource.clip = backgroundMusic[3];
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    public void PlayPowerUpLoop()
    {
        _musicAudioSource.clip = backgroundMusic[4];
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }
}
