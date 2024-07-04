using UnityEngine;

public class DoubleTapDetector : MonoBehaviour
{
    [SerializeField]
    private float maxTimeBetweenTaps = 0.5f; // Maximum time interval between taps to consider it a double tap
    private float lastTapTime = 0f;
    private int tapCount = 0;
    
    void Update()
    {
        if(!GameManager.GameIsPaused)
            DetectDoubleTap();
    }

    void DetectDoubleTap()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                tapCount++;
                if (tapCount == 1)
                {
                    lastTapTime = Time.time;
                }
                else if (tapCount == 2)
                {
                    if (Time.time - lastTapTime <= maxTimeBetweenTaps)
                    {
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


}