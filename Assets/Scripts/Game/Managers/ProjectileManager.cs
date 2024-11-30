using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    private List<GameObject> spawnedProjectiles = new();
    [SerializeField] private List<GameObject> ProjectilesPrefabs = new();

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public List<GameObject> getProjectilesPrefabs()
    {
        return ProjectilesPrefabs;
    }

    public List<GameObject> GetAllProjectiles()
    {
        return new List<GameObject>(spawnedProjectiles);
    } 

    public bool HitProjectile(GameObject projectileObject, float damage)
    {
        if(projectileObject == null) return false;

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        return projectile.TakeDamage(damage, AttackType.None);
    }

    public void DestroyProjectile(GameObject projectileObject)
    {
        if(projectileObject == null) return;
        
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.ActionOnDestroy();
    }

    public void DestroyAllProjectiles()
    {
        foreach (GameObject projectile in spawnedProjectiles)
        {
            DestroyProjectile(projectile);
        }
        spawnedProjectiles = new();
    }

    public void RemoveAllProjectiles()
    {
        foreach (GameObject projectile in spawnedProjectiles)
        {
            Destroy(projectile);
        }
        spawnedProjectiles = new();
    }
     
    public void AddNewProjectile(GameObject newProjectile)
    {
        spawnedProjectiles.Add(newProjectile);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
    }

    public bool NoSpawnedProjectiles()
    {
        RemoveDeletedProjectiles();
        return spawnedProjectiles.Count == 0;
    }

    void RemoveDeletedProjectiles()
    {
        List<GameObject> filteredProjectiles = new List<GameObject>();

        foreach (GameObject projectile in spawnedProjectiles)
        {
            if (projectile != null) 
            {
                filteredProjectiles.Add(projectile);
            }
        }
        spawnedProjectiles = filteredProjectiles;
    }
}
