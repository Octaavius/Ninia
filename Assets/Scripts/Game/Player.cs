using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHitDistance = 500f;
    private Direction hitDirection = Direction.None;
    public LayerMask layersToHit;

    void FixedUpdate()
    {
        if(hitDirection != Direction.None){
            hit(hitDirection);
        }
    }

    void hit(Direction hitDirection){
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.zero;
        switch(hitDirection){
            case Direction.Up:
                direction = Vector2.up;
                break;
            case Direction.Down:
                direction = -Vector2.up;
                break;
            case Direction.Right:
                direction = Vector2.right;
                break;
            case Direction.Left:
                direction = -Vector2.right;
                break;
        }
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxHitDistance, layersToHit);
        
        if (hit.collider != null) {
            Destroy(hit.collider.gameObject);
        }

        setHitDirection(Direction.None);
    }

    public void setHitDirection(Direction hitDirectionToAssign){
        hitDirection = hitDirectionToAssign;
    }
}
