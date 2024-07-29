using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Medkit : Projectile
{
    [SerializeField] private float spawnChance = 0.01f;
    public override float GetSpawnChance() => spawnChance;

    public override void ActionOnCollision(ref Health healthScript)
    {
        Destroy(gameObject);
    }

    public override void ActionOnDestroy()
    {
        GameManager.Instance.ninjaController.healthScript.AddHeart();
        Destroy(gameObject);
    }
}
