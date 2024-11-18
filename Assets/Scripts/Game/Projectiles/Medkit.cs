using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Medkit : Projectile
{
    private float SpawnChance;
    public override float GetSpawnChance() => SpawnChance;
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
