using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioController : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    private AudioSource musicAudioSource;
    
    // Start is called before the first frame update
    private void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMenuMusic();
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
        musicAudioSource.clip = backgroundMusic[0];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    void PlayIntroMusic()
    {
        musicAudioSource.clip = backgroundMusic[1];
        musicAudioSource.Play();
        
        Invoke(nameof(PlayMainLoop), backgroundMusic[1].length);
    }

    void PlayMainLoop()
    {
        musicAudioSource.clip = backgroundMusic[2];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayGhostDownLoop()
    {
        musicAudioSource.clip = backgroundMusic[3];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayPowerUpLoop()
    {
        musicAudioSource.clip = backgroundMusic[4];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
}
