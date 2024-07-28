using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hit))]
[RequireComponent(typeof(NinjaCollision))]
public class NinjaController : MonoBehaviour
{
    [HideInInspector] public Health healthScript;
    [HideInInspector] public Hit hitScript;
    [HideInInspector] public NinjaCollision collisionScript;

    void Awake(){
        healthScript = GetComponent<Health>();
        hitScript = GetComponent<Hit>();
        collisionScript = GetComponent<NinjaCollision>();
    }

    public void InitializeNinja(){
        //set skin or other start fichas
        healthScript.InitializeHearts();
    }

    void FixedUpdate(){
        hitScript.HitCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionScript.OnCollision(collision, ref healthScript);
    }
}
