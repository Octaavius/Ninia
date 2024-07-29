using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GestureDetector))]
public class Hit : MonoBehaviour
{
    private GestureDetector gestureDetector;
    public float timeBetweenMultipleHits = 0.05f;
    [SerializeField] private float maxHitDistance = 500f;
    public LayerMask layersToHit;
    public GameObject afterHitEffect;
    private int numberOfHits = 1;
    
    void Awake(){
    	gestureDetector = GetComponent<GestureDetector>();
    }
    
    public void HitCheck()
    {
        if(gestureDetector.swipeIsDetected()){
            gestureDetector.resetSwipe();
            StartCoroutine(hit());
        }
    }

    private IEnumerator hit(){
        Vector2 origin = transform.position;
        Vector2 direction = gestureDetector.swipeDirectionVector2();
        float actualHitDistance = maxHitDistance;

        if(direction == Vector2.right || direction == Vector2.left) {
            actualHitDistance /= 2;
        }
        float effectZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Instantiate(afterHitEffect, transform.position, Quaternion.Euler(0, 0, effectZRotation));
        for(int i = 0; i < numberOfHits; i++){
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, actualHitDistance, layersToHit);
            
            if (hit.collider != null) { //player hit something
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sliceSound);
                ProjectileManager.Instance.DestroyProjectile(hit.collider.gameObject);
            }     

            yield return new WaitForSeconds(timeBetweenMultipleHits);   
        }
    }

    public void setNumberOfHits(int newNumberOfHits){
        numberOfHits = newNumberOfHits;
    }
}
