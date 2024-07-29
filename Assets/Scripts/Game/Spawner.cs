using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public List<GameObject> projectilesPrefabs;
    public float minSpawnTime = 2.0f;
    public float maxSpawnTime = 3.0f;
    [SerializeField]
    private float maxDelta = 1f;
    [SerializeField]
    private Direction spawnDirection = Direction.Down;
    private bool isPause = false;

    private void Start()
    {
        Debug.Log($"Spawner {gameObject.name} initial minSpawnTime: {minSpawnTime}, maxSpawnTime: {maxSpawnTime}");

        if (SpawnerManager.Instance != null)
        {
            SpawnerManager.Instance.RegisterSpawner(this);
            StartSpawning();
        }
        else
        {
            Debug.LogError("SpawnerManager instance is not set. Ensure SpawnerManager is initialized before Spawner.");
        }
    }

    void StartSpawning()
    {
       Invoke("SpawnProjectiles", Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void TriggerPause()
    {
        StartCoroutine(PauseForSeconds(2f));
    }

    private IEnumerator PauseForSeconds(float seconds)
    {
        isPause = true;
        CancelInvoke("SpawnProjectiles");
        yield return new WaitForSeconds(seconds);
        isPause = false;
        StartSpawning();
    }

    public void OnDifficultyChanged(int newDifficulty)
    {
        TriggerPause();
        SpawnerManager.Instance.AdjustDifficulty(newDifficulty);
        GameObject medkitPrefab = projectilesPrefabs.Find(p => p.GetComponent<Projectile>() is Medkit);
        if (medkitPrefab != null)
        {
            GameObject newMedkit = Instantiate(medkitPrefab, GetSpawnPosition(), GetSpawnDirection());
            ProjectileManager.Instance.AddNewProjectile(newMedkit);
        }
    }
    public Quaternion GetSpawnDirection() //Spawn direction is the direction the projectile will move
    {
        switch (spawnDirection)
        {
            case Direction.Up:
                return Quaternion.Euler(0, 0, 0);
            case Direction.Down:
                return Quaternion.Euler(0, 0, 180);
            case Direction.Left:
                return Quaternion.Euler(0, 0, 90);
            case Direction.Right:
                return Quaternion.Euler(0, 0, -90);
            default:
                return Quaternion.identity;
        }
    }
    public Vector3 GetSpawnPosition() //Spawn position is the position where the projectile will spawn
    {
        float randomDelta = Random.Range(-maxDelta, maxDelta);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 randomMovement;
        switch (spawnDirection)
        {
            case Direction.Up:
            case Direction.Down:
                randomMovement = new Vector2(randomDelta, 0);
                break;
            case Direction.Left:
            case Direction.Right:
                randomMovement = new Vector2(0, randomDelta);
                break;
            default:
                randomMovement = Vector2.zero;
                break;
        }
        Vector2 newPosition = currentPosition + randomMovement;
        return new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
    public void AdjustSpawnRate(float newMinSpawnTime, float newMaxSpawnTime)
    {
        minSpawnTime = newMinSpawnTime;
        maxSpawnTime = newMaxSpawnTime;
    }

    public void SpawnProjectiles()
    {
        List<GameObject> availableProjectiles = new List<GameObject>();
        foreach (var prefab in projectilesPrefabs)
        {
            Projectile projectileScript = prefab.GetComponent<Projectile>();
            if (projectileScript != null && !(projectileScript is Medkit))
            {
                availableProjectiles.Add(prefab);
            }
        }

        if (availableProjectiles.Count == 0)
        {
            Debug.LogWarning("No projectiles available for spawning.");
            return;
        }

        float totalChance = 0f;
        foreach (var prefab in availableProjectiles)
        {
            Projectile projectileScript = prefab.GetComponent<Projectile>();
            totalChance += projectileScript.GetSpawnChance();
        }

        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;
        GameObject selectedProjectile = null;
        foreach (var prefab in availableProjectiles)
        {
            Projectile projectileScript = prefab.GetComponent<Projectile>();
            cumulativeChance += projectileScript.GetSpawnChance();
            if (randomValue <= cumulativeChance)
            {
                selectedProjectile = prefab;
                break;
            }
        }

        if (selectedProjectile != null)
        {
            GameObject newProjectile = Instantiate(selectedProjectile, GetSpawnPosition(), GetSpawnDirection());
            ProjectileManager.Instance.AddNewProjectile(newProjectile);
        }

        if (!isPause)
        {
            StartSpawning();
        }
    }

    public void SpawnCoins()
    {
        GameObject coinPrefab = projectilesPrefabs[0];
        GameObject newCoinProjectile = Instantiate(coinPrefab, GetSpawnPosition(), GetSpawnDirection());
        ProjectileManager.Instance.AddNewProjectile(newCoinProjectile);
        if (!isPause)
        {
            StartSpawning();
        }
    }

    public void SpawnPillows()
    {
        GameObject gameObject = projectilesPrefabs[1];
        GameObject newPillowProjectile = Instantiate(gameObject, GetSpawnPosition(), GetSpawnDirection());
        ProjectileManager.Instance.AddNewProjectile(newPillowProjectile);
        if (!isPause)
        {
            StartSpawning();
        }
    }
}