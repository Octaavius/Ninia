using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow_P : Projectile
{
    public override void ActionOnCollision()
    {
        Destroy(gameObject);
    }
    public override void ActionOnDestroy()
    {
        ActivateBuff<TimeSlow>();
        Destroy(gameObject);
    }
}
