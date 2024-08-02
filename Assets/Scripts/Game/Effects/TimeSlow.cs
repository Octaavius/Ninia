using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : Effect
{
    [SerializeField] private float timeMultiplier = 0.5f;
    [SerializeField] private float audioSpeedMultiplier = 0.8f; 

    private int timeSlowLeanTween;
    private int musicSlowLeanTween;

    protected override void ActivateEffect()
    {
        timeSlowLeanTween = LeanTween.value(gameObject, UpdateTimeScale, Time.timeScale, timeMultiplier, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .id;
        
        musicSlowLeanTween = LeanTween.value(gameObject, UpdatePitch, AudioManager.Instance.musicSource.pitch, audioSpeedMultiplier, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .id;
    }

    protected override void DisactivateEffect()
    {
        LeanTween.cancel(timeSlowLeanTween);
        LeanTween.cancel(musicSlowLeanTween);

        
        LeanTween.value(gameObject, UpdatePitch, AudioManager.Instance.musicSource.pitch, 1f, 1f)
                .setEase(LeanTweenType.easeInOutQuad);
        
        if(GameManager.Instance.GameIsOver || GameManager.Instance.GameIsPaused) return;
        
        LeanTween.value(gameObject, UpdateTimeScale, Time.timeScale, 1f, 1f)
                .setEase(LeanTweenType.easeInOutQuad);
    }

    private void UpdateTimeScale(float value)
    {
        if(GameManager.Instance.GameIsOver){
            LeanTween.cancel(timeSlowLeanTween);
            return;
        }
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
