using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    private List<GameObject> spawnedProjectiles = new();
    [SerializeField] private List<GameObject> ProjectilesPrefabs = new();

    [HideInInspector] public bool projectileWasDestroyed = false;
    private bool showNumbers = false;

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

    public void HitProjectile(GameObject projectileObject){
        if(projectileObject == null) return;

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        string hitResult = projectile.ActionOnHit();
        if(hitResult == "destroyed"){
            projectileWasDestroyed = true;
        }
    }

    public bool ProjectileWasDestroyed(){
        if(projectileWasDestroyed){
            projectileWasDestroyed = false;
            return true;
        } else {
            return false;
        }
    }

    public void DestroyProjectile(GameObject projectileObject){
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
     
    public void AddNewProjectile(GameObject newProjectile){
        spawnedProjectiles.Add(newProjectile);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetShowNumbers(showNumbers);
        }
    }
    public void SetShowNumbers(bool show)
    {
        showNumbers = show;
        spawnedProjectiles.RemoveAll(projectile => projectile == null);
        foreach (var projectile in spawnedProjectiles)
        {
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.SetShowNumbers(show);
            }
        }

        foreach (var prefab in ProjectilesPrefabs)
        {
            Projectile proj = prefab.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.SetShowNumbers(show);
            }
        }
    }
}
