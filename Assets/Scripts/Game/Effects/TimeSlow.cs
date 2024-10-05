using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : Effect
{
    [SerializeField] private float timeMultiplier = 0.5f;
    [SerializeField] private float audioSpeedMultiplier = 0.8f; 

    private int timeSlowLeanTween;
    private int musicSlowLeanTween;
    private bool animationPlaying = false;

    private TimeSlowAnimation timeSlowAnimation;

    protected override void ActivateEffect()
    {
        timeSlowAnimation = FindObjectOfType<TimeSlowAnimation>();
        // Start the slow-motion visuals
        if (timeSlowAnimation != null && animationPlaying == false)
        {
            timeSlowAnimation.StartTimeSlowVisuals();
            animationPlaying = true;
        }


        timeSlowLeanTween = LeanTween.value(gameObject, UpdateTimeScale, Time.timeScale, timeMultiplier, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .id;
        
        musicSlowLeanTween = LeanTween.value(gameObject, UpdatePitch, AudioManager.Instance.musicSource.pitch, audioSpeedMultiplier, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setIgnoreTimeScale(true)
                .id;
    }

    protected override void DeactivateEffect()
    {
        LeanTween.cancel(timeSlowLeanTween);
        LeanTween.cancel(musicSlowLeanTween);

        if (timeSlowAnimation != null && animationPlaying == true)
        {
            timeSlowAnimation.EndTimeSlowVisuals();
            animationPlaying = false;
        }

        LeanTween.value(gameObject, UpdatePitch, AudioManager.Instance.musicSource.pitch, 1f, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setIgnoreTimeScale(true);
        
        if(GameManager.Instance.GameIsOver || GameManager.Instance.GameIsPaused) return;
        
        LeanTween.value(gameObject, UpdateTimeScale, Time.timeScale, 1f, 1f)
                .setEase(LeanTweenType.easeInOutQuad);
    }

    private void UpdateTimeScale(float value)
    {
        if(GameManager.Instance.GameIsPaused){
            return;
        }
        Time.timeScale = value;
    }
    private void UpdatePitch(float value)
    {
        AudioManager.Instance.musicSource.pitch = value;
    }
}
