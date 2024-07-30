using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCoins : Effect
{
    protected override void ActivateEffect(){
        GameManager.Instance.spawnOnlyCoins = true;
    }
    protected override void DisactivateEffect(){
        GameManager.Instance.spawnOnlyCoins = false;
    }
}
