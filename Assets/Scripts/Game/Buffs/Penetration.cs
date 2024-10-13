using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : Buff
{
    protected override void ActivateBuff(){
        GameManager.Instance.ninjaController.hitScript.setNumberOfHits(2);
    }
    protected override void DeactivateBuff(){
        GameManager.Instance.ninjaController.hitScript.setNumberOfHits(1);
    }
}
