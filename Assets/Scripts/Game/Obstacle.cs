using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float Speed = 2f; // Speed of the movement
    [SerializeField]
    private int ScorePrice = 7;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }

    public void setObstacleSpeed(float newSpeed) {
        Speed = newSpeed;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with obstacle!");
            
            // Set a boolean to true or perform any other action
            ActionOnCollision();
        }
    }

    private void ActionOnCollision() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        GameManager.AddToScore(ScorePrice);
    }  
}