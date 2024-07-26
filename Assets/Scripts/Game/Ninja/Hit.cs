using UnityEngine;

[RequireComponent(typeof(GestureDetector))]
public class Hit : MonoBehaviour
{
    private GestureDetector gestureDetector;
    [SerializeField] private float maxHitDistance = 500f;
    public LayerMask layersToHit;
    
    void Awake(){
    	gestureDetector = GetComponent<GestureDetector>();
    }
    
    public void HitCheck()
    {
        if(gestureDetector.swipeIsDetected()){
            gestureDetector.resetSwipe();
            hit();
        }
    }

    void hit(){
        Vector2 origin = transform.position;
        Vector2 direction = gestureDetector.swipeDirectionVector2();
        float actualHitDistance = maxHitDistance;

        if(direction == Vector2.right || direction == Vector2.left) {
            actualHitDistance /= 2;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, actualHitDistance, layersToHit);
        
        if (hit.collider != null) {
            Debug.Log("hit detected");
	        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
            ProjectileManager.Instance.DestroyProjectile(hit.collider.gameObject);
        }        
    }
}
