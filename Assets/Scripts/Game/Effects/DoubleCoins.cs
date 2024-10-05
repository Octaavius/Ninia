using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoins : Effect
{
    public DoubleCoinsAnimation doubleCoinsAnimation;

    protected override void ActivateEffect()
    {
        GameManager.Instance.SetCoinsMultiplier(2);
        doubleCoinsAnimation.ActivateCoinsAnimation(); // Start the animation
    }

    protected override void DeactivateEffect()
    {
        GameManager.Instance.SetCoinsMultiplier(1);
        doubleCoinsAnimation.DeactivateCoinsAnimation(); // Stop the animation
    }
}
