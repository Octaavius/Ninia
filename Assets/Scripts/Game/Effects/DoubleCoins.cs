using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoins : Effect
{
    public override void ActivateEffect(){
        GameManager.Instance.SetCoinsMultiplier(2);
    }
    protected override void DisactivateEffect(){
        GameManager.Instance.SetCoinsMultiplier(1);
    }
}