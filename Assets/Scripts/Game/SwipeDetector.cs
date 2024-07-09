using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField]
    private float minSwipeDistance = 80f;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool stopTouch = false;
    [SerializeField]private DoubleTapDetector doubleTapDetector;
    [SerializeField]private HitScript hitScript;

    void Update() {
        if(!GameManager.GameIsPaused)
            SwipeDetection();
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
                        doubleTapDetector.resetTaps();
                        DetectSwipeDirection();
                    }
                    break;

                case TouchPhase.Ended:
                    stopTouch = false;
                    break;
            }
        }
    }

    void DetectSwipeDirection() {
        Vector2 swipeDirection = currentTouchPosition - startTouchPosition;
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            if (swipeDirection.x > 0)
            {
                hitScript.setHitDirection(Direction.Right);
                Debug.Log("--> Swipe Right");
            }
            else
            {
                hitScript.setHitDirection(Direction.Left);
                Debug.Log("<-- Swipe Left");
            }
        }
        else
        {
            if (swipeDirection.y > 0)
            {
                hitScript.setHitDirection(Direction.Up);
                Debug.Log("Swipe Up");
            }
            else
            {
                hitScript.setHitDirection(Direction.Down);
                Debug.Log("Swipe Down");
            }
        }
    }
}
