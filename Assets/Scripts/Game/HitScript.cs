using UnityEngine;

public class HitScript : MonoBehaviour
{
    public ObstacleManager obstacleManager;
    [SerializeField] private float maxHitDistance = 500f;

    private Direction hitDirection = Direction.None;
    public LayerMask layersToHit;
    public Sound sound;
    void FixedUpdate()
    {
        if(hitDirection != Direction.None){
            hit(hitDirection);
        }
    }

    void hit(Direction hitDirection){
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.zero;
        float actualHitDistance = maxHitDistance;
        switch(hitDirection){
            case Direction.Up:
                direction = Vector2.up;
                break;
            case Direction.Down:
                direction = -Vector2.up;
                break;
            case Direction.Right:
                actualHitDistance = 2;
                direction = Vector2.right;
                break;
            case Direction.Left:
                actualHitDistance = 2;
                direction = -Vector2.right;
                break;
        }
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, actualHitDistance, layersToHit);
        
        if (hit.collider != null) {
            //here i should add score
            obstacleManager.DestroyObstacle(hit.collider.gameObject);
            sound.PlaySliceSound();

        }

        setHitDirection(Direction.None);
    }

    public void setHitDirection(Direction hitDirectionToAssign){
        hitDirection = hitDirectionToAssign;
    }
}
