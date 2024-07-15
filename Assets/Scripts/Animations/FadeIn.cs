using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float animationDuration = 1f;
    public void PlayFadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        // Ensure the image is fully transparent at the start
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        // Start the fade-in animation
        FadeInImage();
    }

    public void FadeInImage()
    {
        // Use LeanTween to fade in the image
        LeanTween.value(gameObject, UpdateColor, 0f, 1f, animationDuration).setEase(LeanTweenType.easeInOutQuad).setIgnoreTimeScale(true);
    }

    void UpdateColor(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
