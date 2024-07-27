using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScore : Effect
{
    public override void ActivateEffect(){
        GameManager.Instance.SetScoreMultiplier(2);
    }
    protected override void DisactivateEffect(){
        GameManager.Instance.SetScoreMultiplier(1);
    }
}