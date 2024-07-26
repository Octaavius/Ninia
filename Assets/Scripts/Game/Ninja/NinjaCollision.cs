using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCollision : MonoBehaviour
{
    public void OnCollision(Collider2D collision, ref AudioManager audioManager, ref Health healthScript, ref GameManager gameManager) 
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        projectile.ActionOnCollision(ref audioManager, ref healthScript, ref gameManager);
    }
}
