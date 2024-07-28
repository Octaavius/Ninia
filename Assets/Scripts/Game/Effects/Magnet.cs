using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magnet : Effect
{
    public static event Action OnMagnetEffectActivatedChanged;

    public override void ActivateEffect()
    {
        GameManager.Instance.magnetEffectActivated = true;
        OnMagnetEffectActivatedChanged?.Invoke();
    }

    protected override void DisactivateEffect()
    {
        GameManager.Instance.magnetEffectActivated = false;
    }
}
