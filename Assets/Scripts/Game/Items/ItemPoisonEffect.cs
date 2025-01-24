using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoisonEffect : Item
{
    [Header("Poison Effect Settings")]
    [SerializeField] private float _poisoningDamage = 30f;

    public override void ApplyEffect(Creature creature){
        creature.AtckScr.AttackModifiers.Add(AttackType.Poison);
        creature.AtckScr.PoisoningDamage = _poisoningDamage;
    }
}
