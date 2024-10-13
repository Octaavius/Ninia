using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magnet : Buff
{
    public static event Action OnMagnetBuffActivatedChanged;

    protected override void ActivateBuff()
    {
        GameManager.Instance.magnetBuffActivated = true;
        OnMagnetBuffActivatedChanged?.Invoke();
    }

    protected override void DeactivateBuff()
    {
        GameManager.Instance.magnetBuffActivated = false;
    }
}
