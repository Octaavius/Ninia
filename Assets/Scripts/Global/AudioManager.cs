using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip collisionSound;
    public AudioClip gameOverSound;
    public AudioClip coinSound;
    public AudioClip sliceSound;
    public AudioClip boomSound;
    public AudioClip punchSound;
    public SceneMusic[] sceneMusicMapping;
    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;  // The name of the scene
        public AudioClip music;  // The corresponding music for the scene
    }

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        Scene currentScene = SceneManager.GetActiveScene();
        PlayMusicForScene(currentScene.name);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);

    }

    private void PlayMusicForScene(string sceneName)
    {
        // Find the music associated with the scene by its name
        foreach (var sceneMusic in sceneMusicMapping)
        {
            if (sceneMusic.sceneName == sceneName)
            {
                // Play the corresponding music
                musicSource.Stop();
                musicSource.clip = sceneMusic.music;
                musicSource.Play();
                break;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
