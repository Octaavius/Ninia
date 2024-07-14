using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleManager obsManager;
    public List<GameObject> obstaclesPrefabs;
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
        Invoke("SpawnObstacle", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnObstacle() {
        // Randomly select an obstacle from the list
        int index = Random.Range(0, obstaclesPrefabs.Count);
        GameObject obstacle = obstaclesPrefabs[index];

        // Spawn the obstacle at the spawner's position
        GameObject newObstacle = Instantiate(obstacle, getSpawnPosition(), getSpawnRotation()); //downward direction
        obsManager.AddNewObstacle(newObstacle);

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