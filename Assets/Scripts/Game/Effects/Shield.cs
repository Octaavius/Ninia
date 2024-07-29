using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Effect
{
    public GameObject ShieldObject;
 
    protected override void ActivateEffect()
    {
        ShieldObject.SetActive(true);
    }

    protected override void DisactivateEffect()
    {
        ShieldObject.SetActive(false);
    }
}
