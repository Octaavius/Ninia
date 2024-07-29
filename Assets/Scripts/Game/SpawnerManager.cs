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
        Debug.Log("Registering spawner: " + spawner.name);
        spawners.Add(spawner);

        // Subscribe to the OnDifficultyChanged event
        if (LevelProgress.Instance != null)
        {
            LevelProgress.Instance.OnDifficultyChanged += spawner.OnDifficultyChanged;
            Debug.Log("Subscribed to OnDifficultyChanged event");
        }
    }
    public void AdjustSpawnRates(int newDifficulty)
    {
        foreach (var spawner in spawners)
        {
            float newMinSpawnTime = Mathf.Max(0.01f, spawner.minSpawnTime - 0.2f * newDifficulty);
            float newMaxSpawnTime = Mathf.Max(0.01f, spawner.maxSpawnTime - 1.0f * newDifficulty);

            Debug.Log($"Setting spawn rate for {spawner.name} to {newMinSpawnTime} - {newMaxSpawnTime}");
            spawner.AdjustSpawnRate(newMinSpawnTime, newMaxSpawnTime);
        }
    }

    public void AdjustProjectileSpeed(int newDifficulty)
    {
        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(2.0f + 0.5f * newDifficulty);
            Debug.Log("Projectile speed set to: " + (2.0f + 0.5f * newDifficulty));
        }
    }

    public void AdjustDifficulty(int newDifficulty)
    {
        if (newDifficulty != currentDifficulty)
        {
            Debug.Log("Adjusting SpawnRate, ProjectileSpeed to: " + newDifficulty);
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