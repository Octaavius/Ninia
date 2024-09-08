using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaisAndSurikenAnimation : MonoBehaviour
{
    public RectTransform panel;            // The panel that contains the rows.
    public float moveSpeed = 50f;          // Speed at which the panel moves.
    public float rowHeight = 260f;         // Height of each row (adjust according to your UI).

    private float distanceMoved = 0f;      // Tracks the distance moved since the last row addition.

    void Update()
    {
        // Move the panel downward
        float moveAmount = moveSpeed * Time.deltaTime;
        panel.anchoredPosition -= new Vector2(0, moveAmount);
        distanceMoved += moveAmount;
        // Check if we've moved a full row's height
        if (distanceMoved >= 2 * rowHeight)
        {
            // Reset distance moved counter
            distanceMoved = 0f;
            panel.anchoredPosition = Vector2.zero;
        }
    }
}
