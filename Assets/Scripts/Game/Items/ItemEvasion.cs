using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvasion : Item
{
    [Header("Evasion Settings")]
    [SerializeField] private float _bonusEvasion = 0.1f;

    public override void ApplyEffect(Creature creature){
        creature.DefScr.EvasionChance += _bonusEvasion;
    }
}
