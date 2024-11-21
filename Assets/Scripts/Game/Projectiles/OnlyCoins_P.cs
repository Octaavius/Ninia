using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCoins_P : Projectile
{
    public override void ActionOnCollision()
    {
        Destroy(gameObject);
    }
    public override void ActionOnDestroy()
    {
        ActivateBuff<OnlyCoins>();
        Destroy(gameObject);
    }
}