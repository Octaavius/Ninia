using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliceTransition : MonoBehaviour
{
    public RectTransform uiElementToBottom;
    public RectTransform uiElementToUp;
    public AnimationCurve customCurve;
    public GameObject slice;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private float screenHeight;
    private float screenWidth;
    [SerializeField] private float movingTime = 1f;
    [SerializeField] private float slicingTime = 0.5f;
    
    [Header("Navigation Buttons To Hide")]
    public RectTransform ShopButton;
    public RectTransform MainButton;
    public RectTransform LevelButton;
    
    void Start() {
        screenHeight = Display.main.systemHeight;
        screenWidth = Display.main.systemWidth;
    }
    public void PlayAnimation() {
        DisableRaycast(uiElementToBottom);
        DisableRaycast(uiElementToUp);
        
        LeanTween.move(ShopButton, new Vector2(ShopButton.anchoredPosition.x, -180), slicingTime).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(MainButton, new Vector2(MainButton.anchoredPosition.x, -180), slicingTime).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(LevelButton, new Vector2(LevelButton.anchoredPosition.x, -180), slicingTime).setEase(LeanTweenType.easeOutQuad);
        
        slice.transform.position = startPosition;

        slice.SetActive(true);

        LeanTween.move(slice, endPosition, slicingTime)
            .setOnComplete(() => {
                UIMoveToBottom();
                UIMoveToUp();
                });
    }

    void UIMoveToBottom()
    {
        // Move the UI element down by half the screen height and to the left by the screen width using DoTween
        Debug.Log(-screenWidth);
        LeanTween.move(uiElementToBottom, new Vector2(-screenWidth, -screenHeight * 0.327f), 1f)
            .setEase(customCurve) // Optional: set the ease type
            .setOnComplete(() => Debug.Log("UI moved down.")); // Optional: callback when animation completes
    }
    void UIMoveToUp()
    {
        // Move the UI element down by half the screen height and to the left by the screen width using DoTween
        Debug.Log(screenWidth);
        LeanTween.move(uiElementToUp, new Vector2(screenWidth, screenHeight * 0.327f), 1f)
            .setEase(customCurve) // Optional: set the ease type
            .setOnComplete(() => Debug.Log("UI moved up.")); // Optional: callback when animation completes
    }

    public float getAnimationTime() {
        return movingTime + slicingTime;
    }

    void DisableRaycast(RectTransform uiElement)
    {
        if (uiElement != null)
        {
            // Get all Graphic components in the RectTransform
            Graphic[] graphics = uiElement.GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                graphic.raycastTarget = false;
            }
        }
    }
}
