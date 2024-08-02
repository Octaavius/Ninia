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
    
    public void HitCheck(ref UltimatePower ulti)
    {
        if(GameManager.Instance.GameIsPaused) return;
        Direction swipeDirection = gestureDetector.DetectSwipe();
        
        if(swipeDirection == Direction.None) return;
        
        Vector2 swipeVector = gestureDetector.DirectionToVector2(swipeDirection);

        StartCoroutine(hit(swipeVector, ulti));
    }

    public bool UltimateActivationTry(){ 
        bool valueToReturn = gestureDetector.doubleTapDetected;
        gestureDetector.doubleTapDetected = false;
        return valueToReturn;
    }

    private IEnumerator hit(Vector2 swipeVector, UltimatePower ulti){
        Vector2 origin = transform.position;
        float actualHitDistance = maxHitDistance;

        if(swipeVector == Vector2.right || swipeVector == Vector2.left) {
            actualHitDistance /= 2;
        }
        float AfterHitEffectZRotation = Mathf.Atan2(swipeVector.y, swipeVector.x) * Mathf.Rad2Deg - 90f;

        Instantiate(afterHitEffect, transform.position, Quaternion.Euler(0, 0, AfterHitEffectZRotation));
        for(int i = 0; i < numberOfHits; i++){
            RaycastHit2D hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, layersToHit);
            
            if (hit.collider != null) { //player hits something
                ulti.AddCharge();
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
