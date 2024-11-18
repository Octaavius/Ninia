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
    [Min(0f)]
    public float Damage = 10f;
    [Min(0f)]
    public float LifeStealPerHit = 0f;
    [Range(0f, 1f)]
    public float CritChance = 0f;
    [Min(0f)]
    public float CritValue = 0f;
    public AttackType[] AttackModifiers;

    private float TickDuration = 1f;
    private float BurningDamage = 20f;
    private float PoisoningDamage = 20f;
    private float FreezeAmount = 0.3f;
    private float HealDecreaseMult = 0.5f;
    private float DebuffDuration = 5f;

    public float CountTotalDamage(){
        float chance = Random.Range(0f, 1f);
        float outDamage = Damage;
        if(chance < CritChance) outDamage *= CritValue;
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
        for(int i = 0; i < 5; i++){
            creature.TakeDamage(BurningDamage, AttackType.Fire);
            yield return new WaitForSeconds(TickDuration);
        }
    }

    private IEnumerator Poisoning(Creature creature){
        // make visual animation of poisoning
        for(int i = 0; i < 5; i++){
            creature.TakeDamage(PoisoningDamage, AttackType.Poison);
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
