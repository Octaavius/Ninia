using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : Buff
{
    protected override void ActivateBuff(){
        NinjaController.Instance.HitScr.SetNumberOfHits(2);
    }
    protected override void DeactivateBuff(){
        NinjaController.Instance.HitScr.SetNumberOfHits(1);
    }
}
