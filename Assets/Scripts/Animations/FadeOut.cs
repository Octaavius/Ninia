using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image fadeImage;
    public float animationDuration = 1f;
    public void PlayFadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        // Ensure the image is fully opaque at the start
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        // Start the fade-out animation
        FadeOutImage();
    }

    public void FadeOutImage()
    {
        // Use LeanTween to fade out the image
        LeanTween.value(gameObject, UpdateColor, 1f, 0f, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => fadeImage.gameObject.SetActive(false));
    }

    void UpdateColor(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
