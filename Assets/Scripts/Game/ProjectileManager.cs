using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private List<GameObject> spawnedProjectiles = new();

    public void DestroyProjectile(GameObject projectileObject){
        spawnedProjectiles.Remove(projectileObject);
        
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.ActionOnDestroy();
    }

    public void DestroyAllProjectiles()
    {
        foreach (GameObject projectile in spawnedProjectiles)
        {
            Destroy(projectile);
        }
        spawnedProjectiles = new();
    }
     
    public void AddNewProjectile(GameObject newProjectile){
        spawnedProjectiles.Add(newProjectile);
    } 
}