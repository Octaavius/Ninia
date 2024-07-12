using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private List<GameObject> spawnedObstacles = new();
    public GameManager gameManager;
    public void DestroyObstacle(GameObject obstacle){
        Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();

        gameManager.AddToScore(obstacleScript.ScorePrice);
        gameManager.AddToCoins(obstacleScript.CoinsPrice);

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
