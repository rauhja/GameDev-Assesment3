using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayIntroMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayIntroMusic()
    {
        audioSource.clip = backgroundMusic[0];
        audioSource.Play();
        
        Invoke(nameof(PlayMainLoop), backgroundMusic[0].length);
    }

    void PlayMainLoop()
    {
        audioSource.clip = backgroundMusic[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGhostDownLoop()
    {
        audioSource.clip = backgroundMusic[2];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayPowerUpLoop()
    {
        audioSource.clip = backgroundMusic[3];
        audioSource.loop = true;
        audioSource.Play();
    }
}
