using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Effect
{
    public GameObject ShieldObject;
    protected override void ActivateEffect()
    {
        ShieldObject.SetActive(true);
        GameManager.Instance.ninjaController.healthScript.shieldActivated = true;
    }

    protected override void DisactivateEffect()
    {
        ShieldObject.SetActive(false);
        GameManager.Instance.ninjaController.healthScript.shieldActivated = false;
    }
}
