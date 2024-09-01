using UnityEngine;
using UnityEngine.UI;

public class D_ScoreAnimation : MonoBehaviour
{
    public FlameAnimator fireAnimation;  // Reference to the FireAnimation script
    public GameObject ScoreMultiplierIcon;   // Reference to the GameObject containing the "2x" symbol and related images for Score
    public GameObject scoreFlameAnimation;    // Reference to the GameObject containing the flame animation for Score

    public void ActivateScoreAnimation()
    {
        // Show the 2x score icon and its flame animation
        ScoreMultiplierIcon.SetActive(true);
        scoreFlameAnimation.SetActive(true);

        // Start the fire animation for the score
        fireAnimation.StartAnimation();
    }

    public void DeactivateScoreAnimation()
    {
        // Hide the 2x score icon and its flame animation
        ScoreMultiplierIcon.SetActive(false);
        scoreFlameAnimation.SetActive(false);

        // Stop the fire animation for the score
        fireAnimation.StopAnimation();
    }
}

