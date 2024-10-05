using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlowAnimation : MonoBehaviour
{
    public GameObject Overlay;   // Assign in the inspector
    public GameObject clock;           // Assign in the inspector
    public Transform hourHand;         // Assign in the inspector
    public Transform minuteHand;       // Assign in the inspector

    public float normalRotationSpeed = 360f;  // Full rotation in one hour for minute hand
    private float currentSpeedMultiplier = 1f;

    void Update()
    {
        // Rotate the hands based on the current speed multiplier
        float minuteRotation = normalRotationSpeed * Time.deltaTime * currentSpeedMultiplier;
        float hourRotation = minuteRotation / 12f;

        minuteHand.Rotate(0, 0, -minuteRotation);
        hourHand.Rotate(0, 0, -hourRotation);
    }

    public void StartTimeSlowVisuals()
    {
        // Purple overlay expands to fill the screen
        LeanTween.scale(Overlay, new Vector3(1, 1, 1), 0.5f)
            .setEase(LeanTweenType.easeOutQuad);

        // Clock expands
        LeanTween.scale(clock, new Vector3(1.0f, 1.0f, 1.0f), 0.5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                StartRotatingHands();
            });
    }

    private void StartRotatingHands()
    {
        // Start rotating the hands at normal speed
        LeanTween.value(gameObject, 1f, 0.5f, 2f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float t) =>
            {
                currentSpeedMultiplier = t;
            })
            .setOnComplete(() =>
            {
                SlowDownRotation();
            });
    }

    private void SlowDownRotation()
    {
        // Slows down the hands to 1/2 speed over 1 second
        LeanTween.value(gameObject, 0.5f, 0.25f, 1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float t) =>
            {
                currentSpeedMultiplier = t;
            });
    }

    private void SpeedUpRotation()
    {
        // Speeds up the hands to normal speed over 1 second
        LeanTween.value(gameObject, 0.25f, 1f, 1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float t) =>
            {
                currentSpeedMultiplier = t;
            });
        
    }

    public void EndTimeSlowVisuals()
    {
        LeanTween.value(gameObject, 0.25f, 1f, 1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float t) =>
            {
                currentSpeedMultiplier = t;
            })
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                // Reverse the purple overlay effect
                LeanTween.scale(Overlay, Vector3.zero, 0.5f)
                    .setEase(LeanTweenType.easeInQuad)
                    .setIgnoreTimeScale(true);

                // Reverse the clock animation and shrink it
                LeanTween.scale(clock, Vector3.zero, 0.5f)
                    .setEase(LeanTweenType.easeInQuad)
                    .setIgnoreTimeScale(true);
            });
    }
}
