using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReflectedDamage : Item
{
    [Header("Armor Settings")]
    [SerializeField] private float _reflectedDamage = 0.2f;

    public override void ApplyEffect(Creature creature){
        creature.DefScr.DamageReflected += _reflectedDamage;
    }
}
