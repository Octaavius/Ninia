using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCollision : MonoBehaviour
{
    ///////////////////////////////////////
    public Health HealthScript;
    public AudioManager am;
    ///////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        projectile.ActionOnCollision(am);
    }
}
