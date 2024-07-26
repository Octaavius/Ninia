using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hit))]
[RequireComponent(typeof(NinjaCollision))]
public class NinjaController : MonoBehaviour
{
    private Health healthScript;
    private Hit hitScript;
    private NinjaCollision collisionScript;

    [Header("Managers")]
    public GameManager gameManager;
    public AudioManager audioManager;
    public ProjectileManager projectileManager;

    void Awake(){
        healthScript = GetComponent<Health>();
        hitScript = GetComponent<Hit>();
        collisionScript = GetComponent<NinjaCollision>();
    }

    void FixedUpdate(){
        hitScript.HitCheck(ref projectileManager, ref audioManager, ref gameManager);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionScript.OnCollision(collision, ref audioManager, ref healthScript, ref gameManager);
    }
}
