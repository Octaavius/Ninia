using UnityEngine;

public class HitScript : MonoBehaviour
{
    ///////////////////////////////////////
    public ProjectileManager projectileManager;
    ///////////////////////////////////////

    public GestureDetector gestureDetector;

    [SerializeField] private float maxHitDistance = 500f;

    public LayerMask layersToHit;
    public AudioManager am;
    void FixedUpdate()
    {
        if(gestureDetector.swipeDetected){
            hit();
        }
    }

    void hit(){
        Vector2 origin = transform.position;
        Vector2 direction = gestureDetector.swipeDirectionVector2;
        float actualHitDistance = maxHitDistance;

        if(direction == Vector2.right || direction == Vector2.left) {
            actualHitDistance /= 3;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, actualHitDistance, layersToHit);
        
        if (hit.collider != null) {
            projectileManager.DestroyProjectile(hit.collider.gameObject);
        }

        gestureDetector.swipeDetected = false;
    }
}
