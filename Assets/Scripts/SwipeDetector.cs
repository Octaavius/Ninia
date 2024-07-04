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
    private DoubleTapDetector doubleTapDetector;
    private PlayerScript playerScript;

    void Awake(){
        doubleTapDetector = GetComponent<DoubleTapDetector>();
        playerScript = GetComponent<PlayerScript>();
    }

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
                playerScript.setHitDirection("right");
                Debug.Log("--> Swipe Right");
                // Call your function for right swipe
            }
            else
            {
                playerScript.setHitDirection("left");
                Debug.Log("<-- Swipe Left");
                // Call your function for left swipe
            }
        }
        else
        {
            if (swipeDirection.y > 0)
            {
                playerScript.setHitDirection("up");
                Debug.Log("Swipe Up");
                // Call your function for up swipe
            }
            else
            {
                playerScript.setHitDirection("down");
                Debug.Log("Swipe Down");
                // Call your function for down swipe
            }
        }
    }
}
