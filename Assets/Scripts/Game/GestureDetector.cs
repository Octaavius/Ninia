using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Up,
    Down,
    Left,
    Right,
    None
}

public class GestureDetector : MonoBehaviour
{

    [Header("Swipe Settings")]
    [SerializeField]
    private float minSwipeDistance = 80f;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool stopTouch = false;

    [Header("Double Tap Settings")]
    [SerializeField]
    private float maxTimeBetweenTaps = 0.5f; // Maximum time interval between taps to consider it a double tap
    private float lastTapTime = 0f;
    private int tapCount = 0;

    public bool swipeDetected = false;
    public Vector2 swipeDirectionVector2;

    void Update() {
        if(!GameManager.GameIsPaused){
            SwipeDetection();
            DetectDoubleTap();
        }
    }

    void SwipeDetection() {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    stopTouch = false;
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    currentTouchPosition = touch.position;
                    float distance = (currentTouchPosition - startTouchPosition).magnitude;
                    if (distance >= minSwipeDistance && !stopTouch)
                    {
                        stopTouch = true;
                        swipeDetected = true;
                        resetTaps();
                        Direction direction = DetectSwipeDirection();
                        swipeDirectionVector2 = DirectionToVector2(direction);
                    }
                    break;

                case TouchPhase.Ended:
                    stopTouch = false;
                    break;
            }
        }
    }

    Direction DetectSwipeDirection() {
        Vector2 swipeDirection = currentTouchPosition - startTouchPosition;
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            if (swipeDirection.x > 0) {
                return Direction.Right;
            } else {
                return Direction.Left;
            }
        } else {
            if (swipeDirection.y > 0) {
                return Direction.Up;
            } else {
                return Direction.Down;
            }
        }
    }

    void DetectDoubleTap() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended) {
                tapCount++;
                if (tapCount == 1) {
                    lastTapTime = Time.time;
                } else if (tapCount == 2) {
                    if (Time.time - lastTapTime <= maxTimeBetweenTaps) {
                        OnDoubleTap();
                    }
                    resetTaps();
                }
            }
        }

        // Reset tap count if time exceeds maxTimeBetweenTaps
        if (tapCount == 1 && Time.time - lastTapTime > maxTimeBetweenTaps)
        {
            resetTaps();
        }
    }

    public void resetTaps(){
        tapCount = 0;
    }

    void OnDoubleTap()
    {
        Debug.Log("Double Tap Detected");
    }

    Vector2 DirectionToVector2(Direction direction){
        switch (direction)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            case Direction.None:
                return Vector2.zero;
            default:
                return Vector2.zero;
        }
    }
}
