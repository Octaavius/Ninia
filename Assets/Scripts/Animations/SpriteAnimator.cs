using System.Collections;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] animationFrames; // Array of sprites for the animation
    public float framesPerSecond = 10f; // Animation speed

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float frameTime;
    private bool isAnimating;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameTime = 1f / framesPerSecond;
        currentFrame = 0;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (currentFrame != animationFrames.Length)
        {
            spriteRenderer.sprite = animationFrames[currentFrame];
            currentFrame = currentFrame + 1;
            yield return new WaitForSeconds(frameTime);
        }
        Destroy(gameObject);
    }
}
