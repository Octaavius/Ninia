using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int Id;

    public abstract void ApplyEffect(Creature creature);
}
