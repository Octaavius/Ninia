using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    private List<Spawner> spawners = new List<Spawner>();
    int currentDifficulty = LevelProgress.Instance.currentDifficulty;

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
    public void AdjustSpawnRates(int newDifficulty)
    {
        foreach (var spawner in spawners)
        {
            float newMinSpawnTime = Mathf.Max(0.5f, spawner.minSpawnTime - 0.05f * newDifficulty);
            float newMaxSpawnTime = Mathf.Max(0.5f, spawner.maxSpawnTime - 0.05f * newDifficulty);

            spawner.AdjustSpawnRate(newMinSpawnTime, newMaxSpawnTime);
        }
    }

    public void AdjustProjectileSpeed(int newDifficulty)
    {
        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(2.0f + 0.1f * newDifficulty);
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

    public void ResetSpawners() // Reset all spawners to default values
    {
        foreach (var spawner in spawners)
        {
            spawner.AdjustSpawnRate(2.0f, 3.0f); // Reset to default spawn times
        }

        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(2.0f); // Reset to default speed
        }
    }
}