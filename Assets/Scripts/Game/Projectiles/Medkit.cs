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
        GameManager.Instance.ninjaController.healthScript.Heal(10);
        Destroy(gameObject);
    }
}
