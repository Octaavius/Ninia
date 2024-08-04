using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public List<GameObject> projectilesPrefabs;
    public Transform spawnPoint;
    private Coroutine spawnCoroutine;
    public float minSpawnTime = 2.0f;
    public float maxSpawnTime = 3.0f;
    [SerializeField]
    private float maxDelta = 1f;
    [SerializeField]
    private Direction spawnDirection = Direction.Down;
    private bool isSpawning = false;


    void Start()
    {
        SpawnerManager.Instance.RegisterSpawner(this);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnProjectiles());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    public void SetSpawnRate(float newMinSpawnTime, float newMaxSpawnTime)
    {
        minSpawnTime = newMinSpawnTime;
        maxSpawnTime = newMaxSpawnTime;
    }

    public void SetSpawnDuration(float duration)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnForDuration(duration));
    }

    public void SetSpawnNumber(int numberOfProjectiles)
    {
        StartCoroutine(SpawnMultipleProjectiles(numberOfProjectiles));
    }

    public void SetSpawnProjectile(GameObject projectilePrefab) //set chosen ONE projectile to spawn
    {
        projectilesPrefabs = new List<GameObject> { projectilePrefab };
    }

    public void SetSpawnProjectiles(List<GameObject> projectilePrefabs) //set chosen MULTIPLE projectiles to spawn
    {
        projectilesPrefabs = projectilePrefabs;
    }

    public void SetSpawnDirection(Direction newDirection)
    {
        spawnDirection = newDirection;
    }

    public void SetSpawnLocation(Vector3 newLocation)
    {
        spawnPoint.position = newLocation;
    }

    public void SetProjectileSpeed(float newSpeed)
    {
        foreach (GameObject prefab in projectilesPrefabs)
        {
            Projectile projectileScript = prefab.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(newSpeed);
        }
    }

    public Quaternion GetSpawnDirection()
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

    public Vector3 GetSpawnPosition()
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

    private IEnumerator SpawnProjectiles()
    {
        while (isSpawning)
        {
            GameObject projectileToSpawn = GetRandomProjectileBasedOnChance();
            if (projectileToSpawn != null)
            {
                GameObject newProjectile = Instantiate(projectileToSpawn, GetSpawnPosition(), GetSpawnDirection());
                ProjectileManager.Instance.AddNewProjectile(newProjectile);
            }
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    private IEnumerator SpawnForDuration(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Instantiate(projectilesPrefabs[Random.Range(0, projectilesPrefabs.Count)], GetSpawnPosition(), GetSpawnDirection());
            elapsedTime += Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
        StopSpawning();
    }

    private IEnumerator SpawnMultipleProjectiles(int numberOfProjectiles)
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Instantiate(projectilesPrefabs[Random.Range(0, projectilesPrefabs.Count)], GetSpawnPosition(), GetSpawnDirection());
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    public void OnDifficultyChanged(int newDifficulty)
    {
        SpawnerManager.Instance.AdjustDifficulty(newDifficulty);
        if(newDifficulty == 0) return;
        /*GameObject medkitPrefab = projectilesPrefabs.Find(p => p.GetComponent<Projectile>() is Medkit);
        if (medkitPrefab != null)
        {
            GameObject newMedkit = Instantiate(medkitPrefab, GetSpawnPosition(), GetSpawnDirection());
            ProjectileManager.Instance.AddNewProjectile(newMedkit);
        }*/
    }
    private GameObject GetRandomProjectileBasedOnChance()
    {
        if (GameManager.Instance.spawnOnlyCoins)
        {
            return projectilesPrefabs.Find(p => p.GetComponent<Projectile>() is Coin);
        }

        float totalChance = 0f;
        foreach (var prefab in projectilesPrefabs)
        {
            Projectile projectile = prefab.GetComponent<Projectile>();
            if (projectile != null)
            {
                totalChance += projectile.spawnChance;
            }
        }

        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (var prefab in projectilesPrefabs)
        {
            Projectile projectile = prefab.GetComponent<Projectile>();
            if (projectile != null)
            {
                cumulativeChance += projectile.spawnChance;
                if (randomValue <= cumulativeChance)
                {
                    return prefab;
                }
            }
        }
        return null; // Should never happen if totalChance is correctly calculated
    }
}