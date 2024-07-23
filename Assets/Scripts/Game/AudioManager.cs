using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Sources------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clips------")]
    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip gamemusic;
    public AudioClip sliceSound;
    public AudioClip coinSound;

    public static AudioManager instance;

   
    private void Awake()
    {
        if ( instance == null)
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
        musicSource.clip = gamemusic;
        musicSource.Play();

    }

    public static void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
