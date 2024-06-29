using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public List<GameObject> obstacles; // List of obstacle prefabs
    public float minSpawnTime = 1.0f; // Minimum spawn time
    public float maxSpawnTime = 3.0f; // Maximum spawn time
    [SerializeField]
    private Direction spawnDirection = Direction.Down;
    private bool isPause = false;

    private void Start()
    {
        StartSpawning();
    }

    void StartSpawning()
    {
        Invoke("SpawnObstacle", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnObstacle()
    {
        // Randomly select an obstacle from the list
        int index = Random.Range(0, obstacles.Count);
        GameObject obstacle = obstacles[index];

        // Spawn the obstacle at the spawner's position
        Instantiate(obstacle, transform.position, getSpawnRotation()); //downward direction

        // Schedule the next spawn
        if(!isPause){
            StartSpawning();
        } 
            
    }

    Quaternion getSpawnRotation()
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
}
