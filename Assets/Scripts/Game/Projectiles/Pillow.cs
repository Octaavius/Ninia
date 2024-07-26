using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Projectile
{
    [SerializeField] private int scorePrice = 12;
    public override void ActionOnCollision(ref AudioManager am, ref Health healthScript, ref GameManager gameManager){
	    am.PlaySFX(am.collisionSound);
        healthScript.RemoveHeart(ref gameManager);
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(ref AudioManager am, ref GameManager gameManager){
        gameManager.AddToScore(scorePrice);
        Destroy(gameObject);
    }
}

