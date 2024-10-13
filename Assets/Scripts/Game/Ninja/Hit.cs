using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum Mode {
    BOSS,
    WAVE
}

[RequireComponent(typeof(GestureDetector))]
public class Hit : MonoBehaviour
{
    [Header("Hit Settings")]
    public float timeBetweenMultipleHits = 0.05f;
    [SerializeField] private float maxHitDistance = 5f;
    [SerializeField] private float damage = 100f;
    public LayerMask layersToHit;
    public GameObject afterHitEffect;
    
    [Header("Boss Fight Settings")]
    [SerializeField] private float MoveDistance = 1.3f;

    [Header("Passive Effects")]
    [SerializeField] private List<GameObject> PositiveEffects; 
    [SerializeField] private List<GameObject> NegativeEffects;

    [HideInInspector] public Mode mode = Mode.WAVE; 
    private GestureDetector gestureDetector;
    private int numberOfHits = 1;    
    private float comboWindowTime = 0.7f;
    private float comboTimer; 
    private Direction firstSwipeDirection;

    void Awake(){
    	gestureDetector = GetComponent<GestureDetector>();
    }

    public void HitCheck(ref UltimatePower ulti)
    {
        if(mode == Mode.WAVE){
            handleSwipesWaveMode(ref ulti);
        } else if(mode == Mode.BOSS) {
            handleSwipesBossMode(ref ulti);
        }
    }

    void handleSwipesBossMode(ref UltimatePower ulti){
        if(!gestureDetector.swipeDetected) return;
        gestureDetector.resetSwipe();
        switch(gestureDetector.swipeDirection){
            case Direction.Up:
                StartCoroutine(DefaultHit(ulti));
                break;
            case Direction.Right:
                MoveToTheRight();
                break;
            case Direction.Left:
                MoveToTheLeft();
                break;
            case Direction.Down:
                ParryAttack();
                break;
        }
    }

    void ParryAttack(){
        //send ray which is blocking and redirecting boss projectiles
    }

    void MoveToTheLeft(){
        if(transform.position.x < -.5f) return;
        MovePlayerHorizontally(-MoveDistance);
    }

    void MoveToTheRight(){
        if(transform.position.x > .5f) return;
        MovePlayerHorizontally(MoveDistance);
    }

    void MovePlayerHorizontally(float horizontalOffset){
        transform.position = new Vector2(transform.position.x + horizontalOffset, transform.position.y);
    }

    void handleSwipesWaveMode(ref UltimatePower ulti){
        UpdateComboTimer();

        if(!gestureDetector.swipeDetected) return;
        gestureDetector.resetSwipe();
        ///////////////////////////////////
        if(SceneManagerScript.Instance.sceneName == "TestScene"){
        //if(SceneManagerScript.Instance.sceneName == "Arcade"){
            if (comboTimer > 0)
            {
                CheckCombo(ref ulti);
                return;
            }
        } else if(SceneManagerScript.Instance.sceneName == "Arcade"){
            //do nothing, cause for now it is just turning off combo
        }
        ///////////////////////////////////
        firstSwipeDirection = gestureDetector.swipeDirection;
        StartCoroutine(DefaultHit(ulti));
    }

    private void CheckCombo(ref UltimatePower ulti){
        Direction lastDirection = gestureDetector.swipeDirection;
        Direction previousDirection = firstSwipeDirection;

        Vector2 lastVector = gestureDetector.DirectionToVector2(lastDirection);
        Vector2 previousVector = gestureDetector.DirectionToVector2(previousDirection);

        float scalarProduct = Vector2.Dot(lastVector, previousVector);
        float vectorProduct = lastVector.x * previousVector.y - lastVector.y * previousVector.x;

        if (scalarProduct == 1)
        {
            StartCoroutine(PenetrationHit(ulti));
            comboTimer = 0;
        }
        else if (scalarProduct == -1)
        {
            StartCoroutine(DefaultHit(ulti));
            firstSwipeDirection = lastDirection;
            comboTimer = comboWindowTime;
        }
        else if (vectorProduct > 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.punchSound);
            ClockwisePunch(ref ulti, true);
            comboTimer = 0;
        }
        else if (vectorProduct < 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.punchSound);
            ClockwisePunch(ref ulti, false);
            comboTimer = 0;
        }
    }

    private void ClockwisePunch(ref UltimatePower ulti, bool clockwise){
        Vector2 origin = transform.position;
        float actualHitDistance = maxHitDistance;

        Vector2 swipeVector = gestureDetector.DirectionToVector2(firstSwipeDirection);
 
        if(swipeVector == Vector2.right || swipeVector == Vector2.left) {
            actualHitDistance /= 2;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, layersToHit);
     
        if (hit.collider != null) { // player hits something
            MoveInEllipseWithLeanTween(hit.collider.transform, clockwise);
        }
    }

    private void MoveInEllipseWithLeanTween(Transform target, bool clockwise) {
        float a; // Semi-major axis
        float b; // Semi-minor axis
        
        float screenWidth = Display.main.systemWidth;
        float screenHeight = Display.main.systemHeight;

        if (firstSwipeDirection == Direction.Right || firstSwipeDirection == Direction.Left) {
            a = Math.Abs(target.position.x);
            // b = a * screenHeight / screenWidth;
            b = 5;
        } else {
            b = Math.Abs(target.position.y);
            //a = b * screenWidth / screenHeight;
            a = 3;
        }
        
        float duration = 0.5f; // Duration of the movement in seconds
        float startAngle = getStartAngle();
        float endAngle = startAngle + (clockwise ? -Mathf.PI / 2 : Mathf.PI / 2);

        float startRotation = target.rotation.eulerAngles.z;

        LeanTween.value(target.gameObject, startAngle, endAngle, duration).setOnUpdate((float angle) => {
            float x = a * Mathf.Cos(angle);
            float y = b * Mathf.Sin(angle);
            target.position = new Vector2(x, y);

            float rotationZ = 0f;
            
            Direction firstSwipeDirectionCopy = firstSwipeDirection;
            if (firstSwipeDirectionCopy == Direction.Right || firstSwipeDirectionCopy == Direction.Left) {
                rotationZ = startRotation + (clockwise ? -1 : 1) * (90f * Math.Abs(Mathf.Sin(angle)));
            } else {
                rotationZ = startRotation + (clockwise ? -1 : 1) * (90f * Math.Abs(Mathf.Cos(angle)));
            }
            target.rotation = Quaternion.Euler(0, 0, rotationZ);
        });
    }

    float getStartAngle(){
        switch(firstSwipeDirection){
            case Direction.Up:
                return Mathf.PI / 2 ;
            case Direction.Down:
                return 3 * Mathf.PI / 2;
            case Direction.Left:
                return Mathf.PI;
            case Direction.Right:
                return 0f;
            default:
                return 0f;
        }
    }

    private IEnumerator DefaultHit(UltimatePower ulti){ // hits numberOfHits times
        Vector2 origin = transform.position;
        float actualHitDistance = maxHitDistance;

        Vector2 swipeVector = gestureDetector.DirectionToVector2(gestureDetector.swipeDirection);
  
        if(swipeVector == Vector2.right || swipeVector == Vector2.left) {
            actualHitDistance /= 2;
        }

        float AfterHitEffectZRotation = Mathf.Atan2(swipeVector.y, swipeVector.x) * Mathf.Rad2Deg - 90f;
        Instantiate(afterHitEffect, transform.position, Quaternion.Euler(0, 0, AfterHitEffectZRotation));

        for(int i = 0; i < numberOfHits; i++){
            RaycastHit2D hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, layersToHit);
            
            if (hit.collider != null) { //player hits something
                comboTimer = comboWindowTime;
                ulti.AddCharge();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sliceSound);
                ProjectileManager.Instance.HitProjectile(hit.collider.gameObject, damage);
                if(ProjectileManager.Instance.ProjectileWasDestroyed(hit.collider.gameObject)){
                    comboTimer = 0;
                }
            }     

            yield return new WaitForSeconds(timeBetweenMultipleHits);   
        }

    }

    private IEnumerator PenetrationHit(UltimatePower ulti){ // kills every enemy in hit direction
        Vector2 origin = transform.position;
        float actualHitDistance = maxHitDistance;

        Vector2 swipeVector = gestureDetector.DirectionToVector2(gestureDetector.swipeDirection);
 

        if(swipeVector == Vector2.right || swipeVector == Vector2.left) {
            actualHitDistance /= 2;
        }

        float AfterHitEffectZRotation = Mathf.Atan2(swipeVector.y, swipeVector.x) * Mathf.Rad2Deg - 90f;
        Instantiate(afterHitEffect, transform.position, Quaternion.Euler(0, 0, AfterHitEffectZRotation));

        bool nextTargetExist = true;
        do {
            RaycastHit2D hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, layersToHit);
            
            if (hit.collider != null) { //player hits something
                ulti.AddCharge();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sliceSound);
                ProjectileManager.Instance.DestroyProjectile(hit.collider.gameObject);
            } else break;

            yield return new WaitForSeconds(timeBetweenMultipleHits);   
        } while (nextTargetExist);
    }

    public bool UltimateActivationTry(){ 
        bool valueToReturn = gestureDetector.doubleTapDetected;
        gestureDetector.doubleTapDetected = false;
        return valueToReturn;
    }

    public void setNumberOfHits(int newNumberOfHits){
        numberOfHits = newNumberOfHits;
    }
    
    void UpdateComboTimer() {
        comboTimer -= Time.deltaTime;
        if(comboTimer <= 0) comboTimer = 0;
    }

    public void changeModeToBossMode(){
        mode = Mode.BOSS;
    }

    public void changeModeToWaveMode(){
        mode = Mode.WAVE;
    }
}
