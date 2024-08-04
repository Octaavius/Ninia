using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_P : Projectile
{
    private float SpawnChance;
    public override float GetSpawnChance() => SpawnChance;
    public override void ActionOnCollision()
    {
        Destroy(gameObject);
    }
    public override void ActionOnDestroy()
    {
        ActivateEffect<Shield>();
        Destroy(gameObject);
    }
}
