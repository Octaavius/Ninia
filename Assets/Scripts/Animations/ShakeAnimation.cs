using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public GameObject background; // Reference to the background object

    public float shakeDuration = 0.5f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 originalCamPos;
    private Vector3 originalBgPos;

    void Start()
    {
        // Store the original positions
        originalCamPos = mainCamera.transform.position;
        originalBgPos = background.transform.position;
    }

    public void TriggerShake()
    {
        // Trigger the explosion effects here

        // Call the shake function after the explosion
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float currentMagnitude = shakeMagnitude * (1 - (elapsed / shakeDuration)); // Damping effect

            float xOffset = Random.Range(-1f, 1f) * currentMagnitude;
            float yOffset = Random.Range(-1f, 1f) * currentMagnitude;

            // Apply shake effect to the camera
            mainCamera.transform.position = new Vector3(originalCamPos.x + xOffset, originalCamPos.y + yOffset, originalCamPos.z);

            // Apply shake effect to the background
            background.transform.position = new Vector3(originalBgPos.x + xOffset, originalBgPos.y + yOffset, originalBgPos.z);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        // Reset to the original positions
        mainCamera.transform.position = originalCamPos;
        background.transform.position = originalBgPos;
    }
}
