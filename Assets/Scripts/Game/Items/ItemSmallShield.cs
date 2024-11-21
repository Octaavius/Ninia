using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour, IItem
{
    [SerializeField] private float BonusArmorAmount = 5f;

    public void ApplyEffect(Creature creature){
        creature.DefScr.Armor += BonusArmorAmount;
    }
}
