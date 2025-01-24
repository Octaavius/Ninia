using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType{
    Fire,
    Poison,
    Freeze,
    Cursed, // decrease hp heal
    None
}

public class Attack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float Damage = 10f;
    public float LifeStealPerHit = 0f;
    [Range(0f, 1f)]
    public float CritChance = 0f;
    public float CritValue = 0f;
    public float PunchDistance = 0f;
    public float OneShotChance = 0f; 

    public List<AttackType> AttackModifiers;

    public float BurningDamage = 20f;
    public float PoisoningDamage = 20f;
    public float FreezeAmount = 0.3f;
    public float HealDecreaseMult = 0.5f;
    
    private float TickDuration = 0.1f;
    private int TickAmount = 20;
    private float DebuffDuration = 5f;

    public float CountTotalDamage(){
        float chance = Random.Range(0f, 1f);
        float outDamage = Damage;
        if(chance < CritChance) outDamage *= CritValue;
        if(chance < OneShotChance) outDamage = 1000000f;
        return outDamage;
    }

    public void ApplyAttackEffects(Creature creature){
        foreach (AttackType atckType in AttackModifiers){
            ApplyEffect(creature, atckType);
        }
    }

    void ApplyEffect(Creature creature, AttackType atckType){
        switch (atckType) {
            case AttackType.Fire:
                StartCoroutine(Burn(creature));
                break;
            case AttackType.Poison:
                StartCoroutine(Poisoning(creature));
                break;
            case AttackType.Freeze:
                StartCoroutine(Freezing(creature));
                break;
            case AttackType.Cursed:
                StartCoroutine(Curse(creature));
                break;       
        }
    }
    
    private IEnumerator Burn(Creature creature){
        // make visual animation of burning
        for(int i = 0; i < TickAmount; i++){
            if(creature.IsDead) yield break;
            creature.TakeDamage(BurningDamage * TickDuration, AttackType.Fire);
            yield return new WaitForSeconds(TickDuration);
        }
    }

    private IEnumerator Poisoning(Creature creature){
        // make visual animation of poisoning
        for(int i = 0; i < TickAmount; i++){
            if(creature.IsDead) yield break;
            creature.TakeDamage(PoisoningDamage * TickDuration, AttackType.Poison);
            yield return new WaitForSeconds(TickDuration);
        }
    }

    private IEnumerator Freezing(Creature creature){
        // make visual animation of freeze
        creature.FreezeDegree += FreezeAmount;
        yield return new WaitForSeconds(DebuffDuration);
        creature.FreezeDegree -= FreezeAmount;
    }

    private IEnumerator Curse(Creature creature){
        // make visual animation of curse
        creature.HpScr.HealMult *= HealDecreaseMult;
        yield return new WaitForSeconds(DebuffDuration);
        creature.HpScr.HealMult *= HealDecreaseMult;
    }
}

// return everything to initial values, create copies of each value in health class
