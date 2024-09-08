using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip collisionSound;
    public AudioClip gameOverSound;
    public AudioClip gameMusic;
    public AudioClip coinSound;
    public AudioClip sliceSound;
    public AudioClip boomSound;
    public AudioClip punchSound;

    public static AudioManager Instance { get; private set; }
   
    private void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
