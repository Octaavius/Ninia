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
    
    public void HitCheck(ref ProjectileManager pm, ref AudioManager am, ref GameManager gm)
    {
        if(gestureDetector.swipeIsDetected()){
            gestureDetector.resetSwipe();
            hit(ref pm, ref am, ref gm);
        }
    }

    void hit(ref ProjectileManager projectileManager, ref AudioManager audioManager, ref GameManager gameManager){
        Vector2 origin = transform.position;
        Vector2 direction = gestureDetector.swipeDirectionVector2();
        float actualHitDistance = maxHitDistance;

        if(direction == Vector2.right || direction == Vector2.left) {
            actualHitDistance /= 2;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, actualHitDistance, layersToHit);
        
        if (hit.collider != null) {
	    audioManager.PlaySFX(audioManager.hitSound);
            projectileManager.DestroyProjectile(hit.collider.gameObject, ref audioManager, ref gameManager);
        }        
    }
}
