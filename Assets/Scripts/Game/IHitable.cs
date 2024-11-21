using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    bool TakeDamage(float damage, AttackType attackType);
}
