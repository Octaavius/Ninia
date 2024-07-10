using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float Speed = 2f; // Speed of the movement
    public int ScorePrice = 7;

    // Update is called once per frame
    void Awake(){
        float zRotation = transform.eulerAngles.z;
        Debug.Log(zRotation);
        if (zRotation == 90f || zRotation == 270f)
        {
            Speed /= 3f;
        }
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }

    public void setObstacleSpeed(float newSpeed) {
        Speed = newSpeed;   
    }
}