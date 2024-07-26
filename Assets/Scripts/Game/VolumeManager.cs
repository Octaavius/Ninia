using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VolumeManager : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    public AudioSource musicAudioSource;
    public AudioSource effectsAudioSource;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float effectsVolume = 1f;

    void Start()
    {
        // Load saved volume values
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        // Set the audio sources and sliders to the saved volume values
        SetVolumes();

        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        effectsVolumeSlider.value = effectsVolume;

        // Add listeners to handle value changes
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
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

    void OnEffectsVolumeChanged(float value)
    {
        effectsVolume = value;
        SetVolumes();
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }

    void SetVolumes()
    {
        musicAudioSource.volume = musicVolume * masterVolume;
        
        effectsAudioSource.volume = effectsVolume * masterVolume;
    }
}
