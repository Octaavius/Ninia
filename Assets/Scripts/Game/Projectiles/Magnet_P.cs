using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet_P : Projectile
{
    private float SpawnChance;
    public override float GetSpawnChance() => SpawnChance;
    public override void ActionOnCollision()
    {
        Destroy(gameObject);
    }

    public override void ActionOnDestroy()
    {
        ActivateBuff<Magnet>();
        Destroy(gameObject);
    }
}
