using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VolumeManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    [Header("Toggles")]
    public Toggle betterUiToggle;

    private AudioSource musicAudioSource;
    private AudioSource effectsAudioSource;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float effectsVolume = 1f;

    void Start()
    {
    	musicAudioSource = AudioManager.Instance.musicSource;
    	effectsAudioSource = AudioManager.Instance.SFXSource;
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

        // Initialize and add listener for better UI toggle
        betterUiToggle.isOn = PlayerPrefs.GetInt("BetterUI", 1) == 1;
        betterUiToggle.onValueChanged.AddListener(OnBetterUiToggleChanged);
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
    private void OnBetterUiToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("BetterUI", isOn ? 1 : 0);
        GameManager.Instance.UpdateBetterUiState(isOn);
    }
}
