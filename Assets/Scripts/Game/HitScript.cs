using UnityEngine;

public class HitScript : MonoBehaviour
{
    public ObstacleManager obstacleManager;
    [SerializeField] private float maxHitDistance = 500f;

    private Direction hitDirection = Direction.None;
    public LayerMask layersToHit;
    public AudioManager AM;
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
            // Check the tag of the hit object
            if (hit.collider.CompareTag("Money"))
            {
                // Play coin sound for coins
                AM.PlaySFX(AM.coinSound);
            }
            else if (hit.collider.CompareTag("Obstacle"))
            {
                // Play slice sound for pillows
                AM.PlaySFX(AM.sliceSound);
            }
            // Destroy the hit object
            obstacleManager.DestroyObstacle(hit.collider.gameObject);


        }

        setHitDirection(Direction.None);
    }

    public void setHitDirection(Direction hitDirectionToAssign){
        hitDirection = hitDirectionToAssign;
    }
}
