using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magnet : Effect
{
    public static event Action OnMagnetEffectActivatedChanged;

    protected override void ActivateEffect()
    {
        GameManager.Instance.magnetEffectActivated = true;
        OnMagnetEffectActivatedChanged?.Invoke();
    }

    protected override void DeactivateEffect()
    {
        GameManager.Instance.magnetEffectActivated = false;
    }
}
