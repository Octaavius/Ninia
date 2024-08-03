using UnityEngine;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour
{
    public RectTransform ShopPanel;  // Leftmost panel
    public RectTransform MainPanel;  // Center panel
    public RectTransform LevelPanel; // Rightmost panel
    private RectTransform currentPanel;

    public float swipeThreshold = 50f; // Threshold for switching panels

    private Vector2 initialTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 previousTouchPosition;
    private bool isDragging = false;

    void Start()
    {
        currentPanel = MainPanel; // Start with the MainPanel as the active panel
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    initialTouchPosition = touch.position;
                    previousTouchPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        currentTouchPosition = touch.position;
                        float deltaX = currentTouchPosition.x - previousTouchPosition.x;

                        // Update the panel position only on the x-axis based on touch movement
                        ShopPanel.anchoredPosition += new Vector2(deltaX, 0);
                        MainPanel.anchoredPosition += new Vector2(deltaX, 0);
                        LevelPanel.anchoredPosition += new Vector2(deltaX, 0);

                        // Update the initial touch position to the current position
                        previousTouchPosition = currentTouchPosition;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    HandleSwipe();
                    break;
            }
        }
    }

    private void HandleSwipe()
    {
        float swipeDeltaX = currentTouchPosition.x - initialTouchPosition.x;

        if (Mathf.Abs(swipeDeltaX) > swipeThreshold)
        {
            if (swipeDeltaX < 0)
            {
                // Swipe to the right
                ChangePanelRight();
            }
            else
            {
                // Swipe to the left
                ChangePanelLeft();
            }
        }

        // Snap back to the current panel's position if no panel change occurred
        currentPanel.anchoredPosition = Vector2.zero;
    }

    private void ChangePanelRight()
    {
        if (currentPanel == MainPanel)
        {
            // Switch to the LevelPanel (right panel)
            currentPanel = LevelPanel;
            MainPanel.anchoredPosition = new Vector2(-Screen.width, 0);
            LevelPanel.anchoredPosition = Vector2.zero;
            ShopPanel.anchoredPosition = new Vector2(-2 * Screen.width, 0);
            Debug.Log("Switched to the right panel (LevelPanel).");
        }
        else if (currentPanel == ShopPanel)
        {
            // Switch to the MainPanel (center panel)
            currentPanel = MainPanel;
            ShopPanel.anchoredPosition = new Vector2(-Screen.width, 0);
            MainPanel.anchoredPosition = Vector2.zero;
            LevelPanel.anchoredPosition = new Vector2(Screen.width, 0);
            Debug.Log("Switched to the center panel (MainPanel).");
        }
        // Do nothing if already on the rightmost panel (LevelPanel)
    }

    private void ChangePanelLeft()
    {
        if (currentPanel == MainPanel)
        {
            // Switch to the ShopPanel (left panel)
            currentPanel = ShopPanel;
            MainPanel.anchoredPosition = new Vector2(Screen.width, 0);
            ShopPanel.anchoredPosition = Vector2.zero;
            LevelPanel.anchoredPosition = new Vector2(Screen.width * 2, 0);
            Debug.Log("Switched to the left panel (ShopPanel).");
        }
        else if (currentPanel == LevelPanel)
        {
            // Switch to the MainPanel (center panel)
            currentPanel = MainPanel;
            LevelPanel.anchoredPosition = new Vector2(Screen.width, 0);
            MainPanel.anchoredPosition = Vector2.zero;
            ShopPanel.anchoredPosition = new Vector2(-Screen.width, 0);
            Debug.Log("Switched to the center panel (MainPanel).");
        }
        // Do nothing if already on the leftmost panel (ShopPanel)
    }
}
