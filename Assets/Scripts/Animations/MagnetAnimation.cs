using UnityEngine;

public class MagnetAnimation : MonoBehaviour
{
    public Vector3 targetPosition;
    public float animationDuration = .5f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    public void doAnimation()
    {
        LeanTween.value(gameObject, 0f, 1f, animationDuration).setOnUpdate((float t) =>
        {
            // Linearly interpolate the y position between start and target positions
            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // Calculate the sinusoidal offset for the x-axis in local space
            float xOffset = Mathf.Sin(t * Mathf.PI);

            // Apply the offset in the object's local space
            Vector3 localOffset = new Vector3(xOffset, 0, 0);
            currentPosition += transform.TransformDirection(localOffset);

            // Update the object's local position
            transform.localPosition = currentPosition;
        });

        LeanTween.scale(gameObject, new Vector3(.2f, .2f, .2f), animationDuration);
    }
}
