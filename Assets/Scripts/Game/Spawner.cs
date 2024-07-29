using UnityEngine;
using System.Collections.Generic;

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
    public void OnDifficultyChanged(int newDifficulty)
    {
        if (SpawnerManager.Instance != null)
        {
           SpawnerManager.Instance.AdjustDifficulty(newDifficulty);
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
        int index = Random.Range(0, projectilesPrefabs.Count);
        GameObject projectilePrefab = projectilesPrefabs[index];
        GameObject newProjectile = Instantiate(projectilePrefab, GetSpawnPosition(), GetSpawnDirection());
        ProjectileManager.Instance.AddNewProjectile(newProjectile);
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