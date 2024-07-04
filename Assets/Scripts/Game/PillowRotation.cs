using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0, 100, 0);


    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
