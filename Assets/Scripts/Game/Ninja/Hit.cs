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
    public float timeBtwMultHits = 0.05f;
    [SerializeField] private float maxHitDistance = 5f;
    public LayerMask mobsLayer;
    public LayerMask bossLayer;
    private LayerMask currentLayer;

    public GameObject afterHitEffect;
    public GameObject afterPunchEffect;

    [Header("Boss Fight Settings")]
    [SerializeField] private float MoveDistance = 1.3f;

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
                PerformHit(ref ulti);
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
        if(SceneManagerScript.Instance.sceneName == "Arcade"){ // can be replaced by some skill which is turning on combos or something else
            if (comboTimer > 0)
            {
                CheckCombo(ref ulti);
                return;
            }
        }
        ///////////////////////////////////
        firstSwipeDirection = gestureDetector.swipeDirection;
        PerformHit(ref ulti);
    }

    void PerformHit(ref UltimatePower ulti, bool penetrateToTheEnd = false){
        StartCoroutine(DefaultHit(ulti, penetrateToTheEnd));
    }

    private void CheckCombo(ref UltimatePower ulti){
        Direction lastDirection = gestureDetector.swipeDirection;
        Vector2 lastVector = gestureDetector.DirectionToVector2(lastDirection);
        Vector2 prevSwipeVector = gestureDetector.DirectionToVector2(firstSwipeDirection);

        float scalarProduct = Vector2.Dot(lastVector, prevSwipeVector);
        float vectorProduct = lastVector.x * prevSwipeVector.y - lastVector.y * prevSwipeVector.x;

        if (scalarProduct == 1)
        {
            PerformHit(ref ulti, true);
            comboTimer = 0;
        }
        else if (scalarProduct == -1)
        {
            PerformHit(ref ulti);
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
        Vector2 swipeVector = gestureDetector.DirectionToVector2(firstSwipeDirection);
        float actualHitDistance = (swipeVector == Vector2.right || swipeVector == Vector2.left) ? maxHitDistance : maxHitDistance / 2;

        Direction lastDirection = gestureDetector.swipeDirection;
        Vector2 lastVector = gestureDetector.DirectionToVector2(lastDirection);
        
        float afterPunchEffectZRotation = Mathf.Atan2(lastVector.y, lastVector.x) * Mathf.Rad2Deg - 90f;
        Instantiate(afterPunchEffect, transform.position, Quaternion.Euler(0, 0, afterPunchEffectZRotation));

        RaycastHit2D hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, currentLayer);
     
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
        
        float duration = 0.3f; // Duration of the movement in seconds
        float startAngle = getStartAngle();
        float endAngle = startAngle + (clockwise ? -Mathf.PI / 2 : Mathf.PI / 2);

        float startRotation = target.rotation.eulerAngles.z;

        Direction firstSwipeDirectionCopy = firstSwipeDirection;
        LeanTween.value(target.gameObject, startAngle, endAngle, duration).setOnUpdate((float angle) => {
            float x = a * Mathf.Cos(angle);
            float y = b * Mathf.Sin(angle);
            target.position = new Vector2(x, y);

            float rotationZ = 0f;
            
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

    private IEnumerator DefaultHit(UltimatePower ulti, bool penetrateToTheEnd = false){
        Vector2 origin = transform.position;
        Vector2 swipeVector = gestureDetector.DirectionToVector2(gestureDetector.swipeDirection);
        float actualHitDistance = (swipeVector == Vector2.right || swipeVector == Vector2.left) ? maxHitDistance : maxHitDistance / 2;

        float AfterHitEffectZRotation = Mathf.Atan2(swipeVector.y, swipeVector.x) * Mathf.Rad2Deg - 90f;
        Instantiate(afterHitEffect, origin, Quaternion.Euler(0, 0, AfterHitEffectZRotation));

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sliceSound);

        RaycastHit2D hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, currentLayer);
        if(hit.collider == null) yield break;
        comboTimer = comboWindowTime;
        ulti.AddCharge();

        LayerMask initialLayerToHit = currentLayer; //if during coroutine current layer is changed to another, initial layer will not be changed
        
        int hitCounter = 0;
        while(true) {
            hit = Physics2D.Raycast(origin, swipeVector, actualHitDistance, initialLayerToHit);
            if(hit.collider == null) {
                break;
            }
            
            IHitable objectToHit = hit.collider.gameObject.GetComponent<IHitable>();

            float damage = NinjaController.Instance.AtckScr.CountTotalDamage();
            bool objectIsDead = objectToHit.TakeDamage(damage, AttackType.None);
            
            if(objectToHit is Creature creature)
                NinjaController.Instance.AtckScr.ApplyAttackEffects(creature);
            
            if(penetrateToTheEnd){
                continue;
            }
            hitCounter++;
            if(hitCounter == numberOfHits){    
                if(objectIsDead){
                    comboTimer = 0;
                }
                break;
            }
            
            yield return new WaitForSeconds(timeBtwMultHits);   
        }
    }

    public bool UltimateActivationTry(){ 
        bool doubleTapDetected = gestureDetector.doubleTapDetected;
        gestureDetector.doubleTapDetected = false;
        return doubleTapDetected;
    }

    public void SetNumberOfHits(int newNumberOfHits){
        numberOfHits = newNumberOfHits;
    }
    
    void UpdateComboTimer() {
        comboTimer -= Time.deltaTime;
        if(comboTimer <= 0) comboTimer = 0;
    }

    public void ChangeModeToBossMode(){
        comboTimer = 0;
        mode = Mode.BOSS;
        currentLayer = bossLayer;
    }

    public void ChangeModeToWaveMode(){
        mode = Mode.WAVE;
        currentLayer = mobsLayer;
    }
}
