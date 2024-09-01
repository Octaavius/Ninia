using UnityEngine;
using UnityEngine.UI;

public class FlameAnimator : MonoBehaviour
{
    public Sprite[] fireFrames;
    public Image targetImage;

    public float frameRate = 0.1f;
    private int currentFrame;
    private float timer;
    private bool isPlaying = false; // Control whether the animation is playing

    private void Start()
    {
        if (fireFrames.Length > 0)
        {
            targetImage.sprite = fireFrames[0];
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;

            if (timer >= frameRate)
            {
                currentFrame = (currentFrame + 1) % fireFrames.Length;
                targetImage.sprite = fireFrames[currentFrame];
                timer = 0f;
            }
        }
    }

    public void StartAnimation()
    {
        isPlaying = true;
        currentFrame = 0;
        timer = 0f;
        targetImage.enabled = true;
    }

    public void StopAnimation()
    {
        isPlaying = false;
        targetImage.enabled = false; // Hide the image when the animation stops
    }
}
