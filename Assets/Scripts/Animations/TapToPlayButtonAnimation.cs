using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayButtonAnimation : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Start the idle animation loop
        StartIdleAnimation();
    }

    void StartIdleAnimation()
    {
        // Sequence the scaling animations with LeanTween for UI elements
        LeanTween.scale(rectTransform, new Vector3(1.2f, 1.2f, 1.2f), 0.2f)
                 .setEase(LeanTweenType.easeInOutQuad)
                 .setOnComplete(() => {
                     LeanTween.scale(rectTransform, new Vector3(1.1f, 1.1f, 1.1f), 0.2f)
                              .setEase(LeanTweenType.easeInOutQuad)
                              .setOnComplete(() => {
                                  LeanTween.scale(rectTransform, new Vector3(1.2f, 1.2f, 1.2f), 0.2f)
                                           .setEase(LeanTweenType.easeInOutQuad)
                                           .setOnComplete(() => {
                                               LeanTween.scale(rectTransform, Vector3.one, 0.2f)
                                                        .setEase(LeanTweenType.easeInOutQuad)
                                                        .setOnComplete(() => {
                                                            // Repeat the animation every 3 seconds
                                                            LeanTween.delayedCall(gameObject, 2f, StartIdleAnimation);
                                                        });
                                           });
                              });
                 });
    }
}
