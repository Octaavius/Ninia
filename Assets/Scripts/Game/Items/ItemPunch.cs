using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPunch : Item
{
    [Header("Punch Settings")]
    [SerializeField] private float _punchDistance = 0.5f;

    public override void ApplyEffect(Creature creature){
        creature.AtckScr.PunchDistance += _punchDistance;
    }
}
