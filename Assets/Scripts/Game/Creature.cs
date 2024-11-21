using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Defence))]
public class Creature : MonoBehaviour, IHitable
{
    [HideInInspector] public Health HpScr;
    [HideInInspector] public Attack AtckScr;
    [HideInInspector] public Defence DefScr;
    [HideInInspector] public float FreezeDegree = 0f;
    public virtual void Awake(){
        HpScr = GetComponent<Health>();
        AtckScr = GetComponent<Attack>();
        DefScr = GetComponent<Defence>();
        Initialize();
    } 

    public virtual void Initialize(){
        HpScr.InitializeHealth();
    }

    public bool TakeDamage(float damage, AttackType attackType){
        bool isDead = HpScr.RemoveHealth(DefScr.ProcessRecievedDamage(damage, attackType));
        if(isDead) ActionOnDestroy();
        return isDead;
    }

    public virtual void ActionOnDestroy() => Destroy(gameObject);
}
