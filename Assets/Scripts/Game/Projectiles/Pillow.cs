using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pillow : Projectile
{
    [Header("Pillow Settings")]
    [SerializeField] private int scorePrice = 12;

    [SerializeField] private int damage = 30;
    
    public override void ActionOnCollision(){
	    AudioManager.Instance.PlaySFX(AudioManager.Instance.collisionSound);
        NinjaController.Instance.TakeDamage(damage, AttackType.None);
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        GameManager.Instance.AddToScore(scorePrice);
        Destroy(gameObject);
    }
}

