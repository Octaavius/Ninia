using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public List<GameObject> MobPrefabs;

    public static MobManager Instance { get; private set; }

    private List<GameObject> SpawnedMobs = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void DestroyAllMobs()
    {
        foreach (GameObject mob in SpawnedMobs)
        {
            if(mob == null) continue;
            mob.GetComponent<Mob>().ActionOnDestroy();
        }
        SpawnedMobs = new();
    }

    public void RemoveAllMobs()
    {
        foreach (GameObject mob in SpawnedMobs)
        {
            Destroy(mob);
        }
        SpawnedMobs = new();
    }
     
    public void AddNewMob(GameObject newMob)
    {
        SpawnedMobs.Add(newMob);
        // if (newMob != null)
        // {
        //     newMob.GetComponent<Mob>().SetShowNumbers(showNumbers);
        // }
    }

    // public void SetShowNumbers(bool show)
    // {
    //     showNumbers = show;
    //     SpawnedMobs.RemoveAll(projectile => projectile == null);
    //     foreach (var projectile in SpawnedMobs)
    //     {
    //         Projectile proj = projectile.GetComponent<Projectile>();
    //         if (proj != null)
    //         {
    //             proj.SetShowNumbers(show);
    //         }
    //     }

    //     foreach (var prefab in ProjectilesPrefabs)
    //     {
    //         Projectile proj = prefab.GetComponent<Projectile>();
    //         if (proj != null)
    //         {
    //             proj.SetShowNumbers(show);
    //         }
    //     }
    // }

    public bool NoSpawnedMobs() 
    {
        RemoveDeletedMobs();
        return SpawnedMobs.Count == 0;
    }

    void RemoveDeletedMobs()
    {
        List<GameObject> filteredMobs = new List<GameObject>();

        foreach (GameObject mob in SpawnedMobs)
        {
            if (mob != null) 
            {
                filteredMobs.Add(mob);
            }
        }
        SpawnedMobs = filteredMobs;
    }
}
