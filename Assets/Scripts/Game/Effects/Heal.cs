using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Effect
{
    protected override void ActivateEffect()
    {
        GameManager.Instance.ninjaController.healthScript.AddHeart();
    }
    protected override void DisactivateEffect(){}
}
