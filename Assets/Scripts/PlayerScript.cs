using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float maxHitDistance = 500f;
    private string hitDirection = " ";
    public LayerMask layersToHit;

    void FixedUpdate()
    {
        if(hitDirection != " "){
            hit(hitDirection);
        }
    }

    void hit(string hitDirection){
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.zero;
        switch(hitDirection){
            case "up":
                direction = Vector2.up;
                break;
            case "down":
                direction = -Vector2.up;
                break;
            case "right":
                direction = Vector2.right;
                break;
            case "left":
                direction = -Vector2.right;
                break;
        }
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxHitDistance, layersToHit);
        
        if (hit.collider != null) {
            Destroy(hit.collider.gameObject);
        }

        setHitDirection(" ");
    }

    public void setHitDirection(string hitDirectionToAssign){
        hitDirection = hitDirectionToAssign;
    }
}
