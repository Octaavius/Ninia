using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    private List<Spawner> spawners = new List<Spawner>();
    public int currentDifficulty;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
        // Subscribe to the OnDifficultyChanged event
        if (LevelProgress.Instance != null)
        {
            LevelProgress.Instance.OnDifficultyChanged += spawner.OnDifficultyChanged;
        }
    }

    public void StartSpawning(params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.StartSpawning();
        }
    }
    public void StopSpawning(params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.StopSpawning();
        }
    }
    /*------------------Spawner managering------------------*/
    public void SetSpawnRate(float minSpawnTime, float maxSpawnTime, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnRate(minSpawnTime, maxSpawnTime);
        }
    }
    public void SetSpawnDuration(float duration, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnDuration(duration);
        }
    }
    public void SetSpawnNumber(int numberOfProjectiles, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnNumber(numberOfProjectiles);
        }
    }
    public void SetSpawnProjectile(GameObject projectilePrefab, params Spawner[] selectedSpawners) //set chosen ONE projectile to spawn
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnProjectile(projectilePrefab);
        }
    }
    public void SetSpawnProjectiles(List<GameObject> projectilePrefabs, params Spawner[] selectedSpawners) //set chosen MULTIPLE projectiles to spawn
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnProjectiles(projectilePrefabs);
        }
    }
    public void SetSpawnDirection(Direction newDirection, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnDirection(newDirection);
        }
    }
    public void SetSpawnLocation(Vector3 newLocation, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnLocation(newLocation);
        }
    }
    public void SetProjectileSpeed(float newSpeed, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetProjectileSpeed(newSpeed);
        }
    }
    /*------------------Difficulty Settings------------------*/
    public void AdjustSpawnRates(int newDifficulty)
    {
        foreach (var spawner in spawners)
        {
            float newMinSpawnTime = Mathf.Max(0.5f, spawner.minSpawnTime - 0.05f * newDifficulty);
            float newMaxSpawnTime = Mathf.Max(0.5f, spawner.maxSpawnTime - 0.05f * newDifficulty);

            spawner.SetSpawnRate(newMinSpawnTime, newMaxSpawnTime);
        }
    }
    public void AdjustProjectileSpeed(int newDifficulty)
    {
        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            float currentSpeed = projectileScript.GetCurrentSpeed();
            projectileScript.SetProjectileSpeed(currentSpeed + 0.01f * newDifficulty);
        }
    }
    public void AdjustDifficulty(int newDifficulty)
    {
        if (newDifficulty != currentDifficulty)
        {
            currentDifficulty = newDifficulty;
            AdjustSpawnRates(newDifficulty);
            AdjustProjectileSpeed(newDifficulty);
        }
    }
    public void ResetSpawners()
    {
        foreach (var spawner in spawners)
        {
            spawner.SetSpawnRate(2.0f, 3.0f);
        }

        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(2.0f);
        }
    }
}