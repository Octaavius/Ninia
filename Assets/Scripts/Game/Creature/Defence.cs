using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Defence : MonoBehaviour
{
    [Header("Defence Settings")]
    [Range(0f, 1f)]
    public float Armor;
    [Range(0f, 1f)]
    public float EvasionChance; 
    [Min(0f)]
    public float DamageReflected;
    public AttackType[] Resistances;

    public float ProcessRecievedDamage(float damage, AttackType attackType){
        float resultingDamage = 0f;
        if(attackType == AttackType.None){
            float randomValue = Random.Range(0f, 1f);
            if(randomValue < EvasionChance) return 0;
            resultingDamage = damage * (1 - Armor);
        } else {
            if(Resistances.Contains(attackType)){
                resultingDamage = damage * 0.1f;                
            }
        }
        return resultingDamage;
    }
}
