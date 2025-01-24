using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOneShot : Item
{
    [Header("One Shot Settings")]
    [SerializeField] private float _oneShotChance = 0.01f;

    public override void ApplyEffect(Creature creature){
        creature.AtckScr.OneShotChance += _oneShotChance;
    }
}
