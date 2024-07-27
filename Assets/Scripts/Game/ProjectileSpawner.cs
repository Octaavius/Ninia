using UnityEngine;
using System.Collections.Generic;

public class ProjectileSpawner : MonoBehaviour
{
    public List<GameObject> projectilesPrefabs;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;
    [SerializeField]
    private float maxDelta = 1f;
    [SerializeField]
    private Direction spawnDirection = Direction.Down;
    private bool isPause = false;
    private float currentProjectileSpeed = 2f; // Default speed

    private void Start() {
        StartSpawning();
        LevelProgress.OnDifficultyChanged += AdjustDifficulty;
    }
    private void OnDestroy()
    {
        LevelProgress.OnDifficultyChanged -= AdjustDifficulty;
    }

    void StartSpawning() {
        Invoke("SpawnProjectile", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnProjectile() {
        // Randomly select an projectile from the list
        int index = Random.Range(0, projectilesPrefabs.Count);
        GameObject projectile = projectilesPrefabs[index];

        // Spawn the projectile at the spawner's position
        GameObject newProjectile = Instantiate(projectile, getSpawnPosition(), getSpawnDirection()); //downward direction


        // Set the speed of the newly spawned projectile
        Projectile proj = newProjectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.setProjectileSpeed(currentProjectileSpeed);
            Debug.Log($"New projectile speed set to {currentProjectileSpeed}");
        }

        ProjectileManager.Instance.AddNewProjectile(newProjectile);

        // Schedule the next spawn
        if(!isPause){
            StartSpawning();
        }  
    }

    Quaternion getSpawnDirection() {
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
    
    Vector3 getSpawnPosition() {
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

    void AdjustDifficulty(int newDifficulty)
    {
        Debug.Log($"Adjusting difficulty to {newDifficulty}");
        minSpawnTime = Mathf.Max(0.01f, 1.0f - 0.3f * newDifficulty);
        maxSpawnTime = Mathf.Max(0.01f, 3.0f - 1.0f * newDifficulty);
        float newSpeed = 2f + 0.5f * newDifficulty;
        AdjustProjectileSpeed(newSpeed);
        currentProjectileSpeed = newSpeed; // Update the speed for future projectiles

    }

    void AdjustProjectileSpeed(float newSpeed)
    {
        foreach (GameObject projectile in ProjectileManager.Instance.GetAllProjectiles())
        {
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.setProjectileSpeed(newSpeed);
                Debug.Log($"Projectile speed set to {newSpeed}");
            }
        }
    }
}
