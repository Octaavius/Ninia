using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlyCoinsAnimation : MonoBehaviour
{
    public GameObject Overlay;  // The rotating background
    public GameObject frame;  // The frame surrounding the TMP label
    public Vector3 targetScale = new Vector3(1f, 1f, 1f); // The target scale (normal size)
    public float rotationSpeed = 5f; // Speed of rotation (degrees per second)

    public void ActivateAnimation()
    {
        // Expand the background
        LeanTween.scale(Overlay, new Vector3(1, 1, 1), 1f)
            .setEase(LeanTweenType.easeOutBack);

        // Start continuous rotation of the background
        RotateBackground();

        // Show and scale the frame
        if (!GameManager.Instance.spawnOnlyCoins) {
            frame.transform.localScale = Vector3.zero;
            LeanTween.scale(frame, targetScale, 0.7f)
                .setEase(LeanTweenType.easeOutBack);
        }
    }

    public void DeactivateAnimation()
    {
        // Stop the rotation by stopping the LeanTween
        LeanTween.cancel(Overlay);

        // Shrink the background
        LeanTween.scale(Overlay, Vector3.zero, 0.5f)
            .setEase(LeanTweenType.easeInBack);

        // Scale out and hide the frame
        LeanTween.scale(frame, Vector3.zero, 0.7f)
            .setEase(LeanTweenType.easeInBack);
    }

    private void RotateBackground()
    {
        // Rotate the background around its z-axis continuously
        LeanTween.rotateAroundLocal(Overlay, Vector3.forward, 360f, rotationSpeed)
            .setEase(LeanTweenType.linear)
            .setRepeat(-1);  // Continue indefinitely
    }
}
