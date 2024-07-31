using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : Effect
{
    [SerializeField] private float timeMultiplier = 0.5f;
    [SerializeField] private float audioSpeedMultiplier = 0.8f;

    protected override void ActivateEffect()
    {
        LeanTween.value(gameObject, UpdateTimeScale, Time.timeScale, timeMultiplier, 1f).setEase(LeanTweenType.easeInOutQuad)
                .setIgnoreTimeScale(true);
        LeanTween.value(gameObject, UpdatePitch, AudioManager.Instance.musicSource.pitch, audioSpeedMultiplier, 1f).setEase(LeanTweenType.easeInOutQuad);
    }

    protected override void DisactivateEffect()
    {
        LeanTween.value(gameObject, UpdatePitch, AudioManager.Instance.musicSource.pitch, 1f, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setIgnoreTimeScale(true);
        
        if(GameManager.Instance.GameIsOver || GameManager.Instance.GameIsPaused) return;
        
        LeanTween.value(gameObject, UpdateTimeScale, Time.timeScale, 1f, 1f)
                .setEase(LeanTweenType.easeInOutQuad);
    }

    private void UpdateTimeScale(float value)
    {
        Time.timeScale = value;
    }
    private void UpdatePitch(float value)
    {
        AudioManager.Instance.musicSource.pitch = value;
    }
}
