using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Projectile
{
    [SerializeField] private int scorePrice = 12;
    [SerializeField] private float spawnChance = 0.9f;
    public override float GetSpawnChance() => spawnChance;
    public override void ActionOnCollision(){
	    AudioManager.Instance.PlaySFX(AudioManager.Instance.collisionSound);
        GameManager.Instance.ninjaController.healthScript.RemoveHeart();
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        GameManager.Instance.AddToScore(scorePrice);
        Destroy(gameObject);
    }
}

