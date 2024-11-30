using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hit))]
[RequireComponent(typeof(UltimatePower))]
public class NinjaController : Creature
{
    [HideInInspector] public Hit HitScr;
    [HideInInspector] public UltimatePower Ulti;

    public static NinjaController Instance { get; private set; }

    public override void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        HitScr = GetComponent<Hit>();
        Ulti = GetComponent<UltimatePower>();
        base.Awake();
    }

    public override void Initialize(){
        base.Initialize();
        Ulti.ResetUltimatePower();
    }

    void FixedUpdate(){
        HitScr.HitCheck(ref Ulti);
        if(HitScr.UltimateActivationTry()){
            Ulti.TryActivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collObject = collision.gameObject;
        if(collObject.GetComponent<Mob>() != null){
            collObject.GetComponent<Mob>().ActionOnCollisionWithNinja();
        } else {
            Projectile projectile = collision.GetComponent<Projectile>();
            projectile.ActionOnCollision();
        }
    }

    public override void ActionOnDestroy(){
        GameManager.Instance.EndGame();
    }
}
