using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScore : Effect
{
    public D_ScoreAnimation doubleScoreAnimation;
    protected override void ActivateEffect(){
        GameManager.Instance.SetScoreMultiplier(2);
        doubleScoreAnimation.ActivateScoreAnimation();
    }
    protected override void DisactivateEffect(){
        GameManager.Instance.SetScoreMultiplier(1);
        doubleScoreAnimation.DeactivateScoreAnimation();
    }
}
