using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCollision : MonoBehaviour
{
    public void OnCollision(Collider2D collision, ref Health healthScript) 
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        projectile.ActionOnCollision(ref healthScript);
    }
}
