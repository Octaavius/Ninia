using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : Effect
{
    protected override void ActivateEffect(){
        GameManager.Instance.ninjaController.hitScript.setNumberOfHits(2);
    }
    protected override void DisactivateEffect(){
        GameManager.Instance.ninjaController.hitScript.setNumberOfHits(1);
    }
}
