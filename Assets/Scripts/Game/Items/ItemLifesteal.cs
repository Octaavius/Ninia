using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLifesteal : Item
{
    [Header("Lifesteal Settings")]
    [SerializeField] private float _lifesteal = 1f;

    public override void ApplyEffect(Creature creature){
        creature.AtckScr.LifeStealPerHit += _lifesteal;
    }
}
