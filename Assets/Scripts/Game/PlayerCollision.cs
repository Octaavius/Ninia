using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Health HealthScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionOnCollision(collision);
    }

    private void ActionOnCollision(Collider2D collision) {
        HealthScript.RemoveHeart();
        Destroy(collision.gameObject);
    } 
}
