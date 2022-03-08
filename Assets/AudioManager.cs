using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;
    public bool isStopped = false;

    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex+1)%playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

}
