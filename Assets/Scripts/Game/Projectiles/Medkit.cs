using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Medkit : Projectile
{
    public override void ActionOnCollision()
    {
        Destroy(gameObject);
    }

    public override void ActionOnDestroy()
    {
        NinjaController.Instance.HpScr.Heal(10);
        Destroy(gameObject);
    }
}
