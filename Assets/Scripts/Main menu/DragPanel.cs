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

    private int mainLeanTween = -1;
    private int shopLeanTween = -1;
    private int levelLeanTween = -1;

    void Start()
    {
        currentPanel = MainPanel; // Start with the MainPanel as the active panel
    }

    void Update()
    {
        if (SceneManagerMenu.Instance.transitionStarted) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    currentTouchPosition = touch.position;
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
                ChangePanelRight();
            }
            else
            {
                ChangePanelLeft();
            }
        }
        ResetPanelsPosition();
    }

    private void ResetPanelsPosition()
    {
        Vector2 mainTargetPos = Vector2.zero;
        Vector2 levelTargetPos = Vector2.zero;
        Vector2 shopTargetPos = Vector2.zero;

        if (currentPanel == MainPanel)
        {
            shopTargetPos = new Vector2(-Screen.width, 0);
            mainTargetPos = Vector2.zero;
            levelTargetPos = new Vector2(Screen.width, 0);
        }
        else if (currentPanel == LevelPanel)
        {
            mainTargetPos = new Vector2(-Screen.width, 0);
            levelTargetPos = Vector2.zero;
            shopTargetPos = new Vector2(-2 * Screen.width, 0);
        }
        else if (currentPanel == ShopPanel)
        {
            mainTargetPos = new Vector2(Screen.width, 0);
            shopTargetPos = Vector2.zero;
            levelTargetPos = new Vector2(2 * Screen.width, 0);
        }

        float transitionDuration = 0.2f; // Duration of the transition in seconds

        mainLeanTween = LeanTween.move(MainPanel, mainTargetPos, transitionDuration).setEase(LeanTweenType.easeOutQuad).id;
        shopLeanTween = LeanTween.move(LevelPanel, levelTargetPos, transitionDuration).setEase(LeanTweenType.easeOutQuad).id;
        levelLeanTween = LeanTween.move(ShopPanel, shopTargetPos, transitionDuration).setEase(LeanTweenType.easeOutQuad).id;
    }

    private void ChangePanelRight()
    {
        if (currentPanel == MainPanel)
        {
            ChangePanelToLevelPanel();
        }
        else if (currentPanel == ShopPanel)
        {
            ChangePanelToMainPanel();
        } else {
            ResetPanelsPosition();
        }
    }

    private void ChangePanelLeft()
    {
        if (currentPanel == MainPanel)
        {
            ChangePanelToShopPanel();
        }
        else if (currentPanel == LevelPanel)
        {
            ChangePanelToMainPanel();
        } else {
            ResetPanelsPosition();
        }
    }
    
    public void ChangePanelToMainPanel(){
	currentPanel = MainPanel;
        ResetPanelsPosition();
    }
    public void ChangePanelToShopPanel(){
	currentPanel = ShopPanel;
        ResetPanelsPosition();
    }
    public void ChangePanelToLevelPanel(){
	currentPanel = LevelPanel;
        ResetPanelsPosition();
    }
    
    
}
