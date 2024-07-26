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

    private void Start() {
        StartSpawning();
    }

    void StartSpawning() {
        Invoke("SpawnProjectile", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnProjectile() {
        // Randomly select an projectile from the list
        int index = Random.Range(0, projectilesPrefabs.Count);
        GameObject projectile = projectilesPrefabs[index];

        // Spawn the projectile at the spawner's position
        GameObject newProjectile = Instantiate(projectile, getSpawnPosition(), getSpawnRotation()); //downward direction
      
        ProjectileManager.Instance.AddNewProjectile(newProjectile);

        // Schedule the next spawn
        if(!isPause){
            StartSpawning();
        }  
    }

    

    Quaternion getSpawnRotation() {
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
}
