using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamageForHealth : Item
{
    [Header("Damage For Health Settings")]
    [SerializeField] private float _bonusDamage = 50f;
    [SerializeField] private float _healthSacrifice = 30f;

    public override void ApplyEffect(Creature creature){
        creature.HpScr.MaxHp -= _healthSacrifice;
        creature.AtckScr.Damage += _bonusDamage;
    }
}
