using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_P : Projectile
{
    public override void ActionOnCollision()
    {
        Destroy(gameObject);
    }
    public override void ActionOnDestroy()
    {
        ActivateBuff<Shield>();
        Destroy(gameObject);
    }
}
