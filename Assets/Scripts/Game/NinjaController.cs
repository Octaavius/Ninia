using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hit))]
[RequireComponent(typeof(NinjaCollision))]
[RequireComponent(typeof(UltimatePower))]
public class NinjaController : MonoBehaviour
{
    [HideInInspector] public Health healthScript;
    [HideInInspector] public Hit hitScript;
    [HideInInspector] public NinjaCollision collisionScript;
    [HideInInspector] public UltimatePower ulti;

    void Awake(){
        healthScript = GetComponent<Health>();
        hitScript = GetComponent<Hit>();
        collisionScript = GetComponent<NinjaCollision>();
        ulti = GetComponent<UltimatePower>();
    }

    public void InitializeNinja(){
        healthScript.InitializeHearts();
    }

    void FixedUpdate(){
        if(hitScript.UltimateActivationTry()){
            ulti.TryActivate();
        }
        hitScript.HitCheck(ref ulti);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionScript.OnCollision(collision, ref healthScript);
    }
}
