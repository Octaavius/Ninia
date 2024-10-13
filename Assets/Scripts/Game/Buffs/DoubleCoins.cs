using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoins : Buff
{
    public DoubleCoinsAnimation doubleCoinsAnimation;

    protected override void ActivateBuff()
    {
        GameManager.Instance.SetCoinsMultiplier(2);
        doubleCoinsAnimation.ActivateCoinsAnimation(); // Start the animation
    }

    protected override void DeactivateBuff()
    {
        GameManager.Instance.SetCoinsMultiplier(1);
        doubleCoinsAnimation.DeactivateCoinsAnimation(); // Stop the animation
    }
}
