using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamage : Item
{
    [Header("Damage Settings")]
    [SerializeField] private float _bonusDamage = 50f;

    public override void ApplyEffect(Creature creature){
        creature.AtckScr.Damage += _bonusDamage;
    }
}
