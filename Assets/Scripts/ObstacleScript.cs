using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Speed of the movement
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the object forward based on its current direction and speed
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    public void setObstacleSpeed(float newSpeed) {
        
    }  
}
