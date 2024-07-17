using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCollision : MonoBehaviour
{
    [SerializeField] private Health HealthScript;
    public AudioManager AM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionOnCollision(collision);
    }

    private void ActionOnCollision(Collider2D collision) {
        if (collision.CompareTag("Money")) {
            //dont do anything, because we will destroy it later
        } else if (collision.CompareTag("Obstacle")) {
            AM.PlaySFX(AM.hitSound);
            HealthScript.RemoveHeart();
        }
        Destroy(collision.gameObject);
    } 
}
