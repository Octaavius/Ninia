using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArmor : Item
{
    [Header("Armor Settings")]
    [SerializeField] private float _bonusArmor = 0.05f;

    public override void ApplyEffect(Creature creature){
        creature.DefScr.Armor += _bonusArmor;
    }
}
