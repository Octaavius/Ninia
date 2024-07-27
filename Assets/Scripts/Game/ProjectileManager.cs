using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    private List<GameObject> spawnedProjectiles = new();
    [SerializeField] private List<GameObject> ProjectilesPrefabs = new();

    void Awake(){
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
