using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFireEffect : Item
{
    [Header("Fire Effect Settings")]
    [SerializeField] private float _burningDamage = 30f;

    public override void ApplyEffect(Creature creature){
        creature.AtckScr.AttackModifiers.Add(AttackType.Fire);
        creature.AtckScr.BurningDamage = _burningDamage;
    }
}
