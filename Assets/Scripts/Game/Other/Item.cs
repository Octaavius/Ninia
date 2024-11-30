using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemZone{
    Head,
    Face,
    LeftHand,
    RightHand,
    LeftWaist,
    RightWaist,
    LeftLeg,
    RightLeg,
    None
}

public enum ItemType{
    Head,
    Face,
    Hand,
    Waist,
    Leg,
    None
}

public abstract class Item : MonoBehaviour
{
    [Header("Menu Settings")]
    public int Id;
    public ItemType Type;

    public abstract void ApplyEffect(Creature creature);
}
