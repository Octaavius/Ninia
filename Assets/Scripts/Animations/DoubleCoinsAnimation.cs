using UnityEngine;
using UnityEngine.UI;

public class DoubleCoinsAnimation : MonoBehaviour
{
    public FlameAnimator fireAnimation;  // Reference to the FireAnimation script
    public GameObject CoinsMultiplierIcon;   // Reference to the GameObject containing the "2x" symbol and related images for Coins
    public GameObject coinsFlameAnimation;    // Reference to the GameObject containing the flame animation for Coins

    public void ActivateCoinsAnimation()
    {
        // Show the 2x coins icon and its flame animation
        CoinsMultiplierIcon.SetActive(true);
        coinsFlameAnimation.SetActive(true);

        // Start the fire animation for the coins
        fireAnimation.StartAnimation();
    }

    public void DeactivateCoinsAnimation()
    {
        // Hide the 2x coins icon and its flame animation
        CoinsMultiplierIcon.SetActive(false);
        coinsFlameAnimation.SetActive(false);

        // Stop the fire animation for the coins
        fireAnimation.StopAnimation();
    }
}

