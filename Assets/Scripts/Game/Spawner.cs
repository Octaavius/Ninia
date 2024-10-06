using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    private List<GameObject> projectilePrefabs;
    public Transform spawnPoint;
    private Coroutine spawnCoroutine = null;
    public float minSpawnTime = 2.0f;
    public float maxSpawnTime = 3.0f;
    [SerializeField]
    private float maxDelta = 1f;
    [SerializeField]
    private Direction spawnDirection = Direction.Down;
    private bool isSpawning = false;
    private float originalMinSpawnTime;
    private float originalMaxSpawnTime;
    private float originalProjectileSpeed;


    void Start()
    {
        SpawnerManager.Instance.RegisterSpawner(this);
        projectilePrefabs = SpawnerManager.Instance.projectilePrefabs; 
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

    public void SetSpawnProjectile(GameObject chosenProjectilePrefab) //set chosen ONE projectile to spawn
    {
        projectilePrefabs = new List<GameObject> { chosenProjectilePrefab };
    }

    public void SetSpawnProjectiles(List<GameObject> chosenProjectilePrefabs) //set chosen MULTIPLE projectiles to spawn
    {
        projectilePrefabs = chosenProjectilePrefabs;
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
        foreach (GameObject prefab in projectilePrefabs)
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
            Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Count)], GetSpawnPosition(), GetSpawnDirection());
            elapsedTime += Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
        StopSpawning();
    }

    private IEnumerator SpawnMultipleProjectiles(int numberOfProjectiles)
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Count)], GetSpawnPosition(), GetSpawnDirection());
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    public void OnDifficultyChanged(int newDifficulty)
    {
        SpawnerManager.Instance.AdjustDifficulty(newDifficulty);
        if(newDifficulty == 0) return;
        /*GameObject medkitPrefab = projectilePrefabs.Find(p => p.GetComponent<Projectile>() is Medkit);
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
            // Store the original values if not already stored
            if (originalMinSpawnTime == 0 && originalMaxSpawnTime == 0 && originalProjectileSpeed == 0)
            {
                originalMinSpawnTime = minSpawnTime;
                originalMaxSpawnTime = maxSpawnTime;
                if (projectilePrefabs.Count > 0)
                {
                    Projectile projectileScript = projectilePrefabs[0].GetComponent<Projectile>();
                    if (projectileScript != null)
                    {
                        originalProjectileSpeed = projectileScript.GetCurrentSpeed();
                    }
                }
            }

            // Change the spawn rate and projectile speed
            SetSpawnRate(0.1f, 1.0f); // Example values
            SetProjectileSpeed(3.0f); // Example value

            return projectilePrefabs.Find(p => p.GetComponent<Projectile>() is Coin);

        }
        else
        {
            // Reset the values to their original state
            if (originalMinSpawnTime != 0 && originalMaxSpawnTime != 0 && originalProjectileSpeed != 0)
            {
                SetSpawnRate(originalMinSpawnTime, originalMaxSpawnTime);
                SetProjectileSpeed(originalProjectileSpeed);

                // Clear the original values
                originalMinSpawnTime = 0;
                originalMaxSpawnTime = 0;
                originalProjectileSpeed = 0;
            }
        }

        float totalChance = 0f;
        foreach (var prefab in projectilePrefabs)
        {
            Projectile projectile = prefab.GetComponent<Projectile>();
            if (projectile != null)
            {
                totalChance += projectile.spawnChance;
            }
        }

        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (var prefab in projectilePrefabs)
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