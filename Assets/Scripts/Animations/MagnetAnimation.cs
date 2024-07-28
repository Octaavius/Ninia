using UnityEngine;

public class MagnetAnimation : MonoBehaviour
{
    public Vector3 targetPosition; // Target position where the object will move
    public float animationDuration = .5f; // Duration of the animation

    public void doAnimation()
    {
        // Move to the target position with ease-in acceleration and change scale to zero
        LeanTween.move(gameObject, targetPosition, animationDuration);

        // Scale to zero over the same duration
        LeanTween.scale(gameObject, new Vector3(.2f, .2f, .2f), animationDuration);
    }
}
