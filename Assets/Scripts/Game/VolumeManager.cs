using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;

    public AudioSource musicAudioSource;
    public AudioSource SFXAudioSource;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float SFXVolume = 1f;

    void Start()
    {
        // Load saved volume values
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Set the audio sources and sliders to the saved volume values
        SetVolumes();

        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        SFXVolumeSlider.value = SFXVolume;

        // Add listeners to handle value changes
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    void OnMasterVolumeChanged(float value)
    {
        masterVolume = value;
        SetVolumes();
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    void OnMusicVolumeChanged(float value)
    {
        musicVolume = value;
        SetVolumes();
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    void OnSFXVolumeChanged(float value)
    {
        SFXVolume = value;
        SetVolumes();
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }

    void SetVolumes()
    {
        musicAudioSource.volume = musicVolume * masterVolume;
        SFXAudioSource.volume = SFXVolume * masterVolume;
    }
}
