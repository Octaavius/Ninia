using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScore : Buff
{
    public D_ScoreAnimation doubleScoreAnimation;
    protected override void ActivateBuff(){
        GameManager.Instance.SetScoreMultiplier(2);
        doubleScoreAnimation.ActivateScoreAnimation();
    }
    protected override void DeactivateBuff(){
        GameManager.Instance.SetScoreMultiplier(1);
        doubleScoreAnimation.DeactivateScoreAnimation();
    }
}
