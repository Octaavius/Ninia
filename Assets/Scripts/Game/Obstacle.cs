using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Speed of the movement
    [SerializeField]
    private int scorePrice = 7;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    public void setObstacleSpeed(float newSpeed) {
        speed = newSpeed;   
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

    private void ActionOnCollision()
    {
        GameManager.AddToScore(scorePrice);
        Destroy(gameObject);
    }  
}