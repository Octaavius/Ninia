using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private List<GameObject> spawnedObstacles = new();
    public GameManager gameManager;
    public void DestroyObstacle(GameObject obstacle){
        gameManager.AddToScore(obstacle.GetComponent<Obstacle>().ScorePrice);
        spawnedObstacles.Remove(obstacle);
        Destroy(obstacle);
    }

    public void DestroyAllObstacles()
    {
        foreach (GameObject obj in spawnedObstacles)
        {
            Destroy(obj);
        }
        spawnedObstacles = new();
    }
     
    public void AddNewObstacle(GameObject newObstacle){
        spawnedObstacles.Add(newObstacle);
    } 
}
