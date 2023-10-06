using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    private AudioSource musicAudioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
        PlayIntroMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayIntroMusic()
    {
        musicAudioSource.clip = backgroundMusic[0];
        musicAudioSource.Play();
        
        Invoke(nameof(PlayMainLoop), backgroundMusic[0].length);
    }

    void PlayMainLoop()
    {
        musicAudioSource.clip = backgroundMusic[1];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayGhostDownLoop()
    {
        musicAudioSource.clip = backgroundMusic[2];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayPowerUpLoop()
    {
        musicAudioSource.clip = backgroundMusic[3];
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
}
