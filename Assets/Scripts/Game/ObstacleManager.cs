using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private List<GameObject> spawnedProjectiles = new();

    public void DestroyProjectile(Projectile projectile){
        spawnedProjectiles.Remove(projectile);
        
        Projectile projectile = projectile.GetComponent<Projectile>();
        projectile.ActionOnDestroy();
    }

    public void DestroyAllProjectiles()
    {
        foreach (Projectile projectile in spawnedProjectiles)
        {
            GameObject projectileObject = projectile.gameObject;
            Destroy(projectileObject);
        }
        spawnedProjectiles = new();
    }
     
    public void AddNewProjectile(Projectile newProjectile){
        spawnedProjectiles.Add(newProjectile);
    } 
}
