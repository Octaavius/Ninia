using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Projectile
{
    [SerializeField] private int scorePrice = 12;
    private float SpawnChance;
    public override float GetSpawnChance() => SpawnChance;
    public override void ActionOnCollision(){
	    AudioManager.Instance.PlaySFX(AudioManager.Instance.collisionSound);
        GameManager.Instance.ninjaController.healthScript.RemoveHealth(30);
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        GameManager.Instance.AddToScore(scorePrice);
        Destroy(gameObject);
    }
}

