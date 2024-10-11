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
    
    [HideInInspector] public bool transitionStarted = false;

    public static SliceTransition Instance { get; private set; }
    
    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        screenHeight = Display.main.systemHeight;
        screenWidth = Display.main.systemWidth;
    }
    
    public void TestAnimation(){
	transitionStarted = true;
        DisableRaycast(uiElementToBottom);
        DisableRaycast(uiElementToUp);
        
        slice.transform.position = startPosition;

        slice.SetActive(true);

        LeanTween.move(slice, endPosition, slicingTime)
            .setOnComplete(() => {
                UIMoveToBottom();
                UIMoveToUp();
                });
    }

    public void PlayAnimation() {
        transitionStarted = true;
        DisableRaycast(uiElementToBottom);
        DisableRaycast(uiElementToUp);
        
        HideButtons();
        
        slice.transform.position = startPosition;

        slice.SetActive(true);

        LeanTween.move(slice, endPosition, slicingTime)
            .setOnComplete(() => {
                UIMoveToBottom();
                UIMoveToUp();
                });
    }
    
    void HideButtons(){
    	LeanTween.move(ShopButton, new Vector2(ShopButton.anchoredPosition.x, -180), slicingTime).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(MainButton, new Vector2(MainButton.anchoredPosition.x, -180), slicingTime).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(LevelButton, new Vector2(LevelButton.anchoredPosition.x, -180), slicingTime).setEase(LeanTweenType.easeOutQuad);
    }

    void UIMoveToBottom()
    {
        LeanTween.move(uiElementToBottom, new Vector2(-screenWidth, -screenHeight * 0.327f), movingTime)
            .setEase(customCurve)
            .setOnComplete(() => {
                transitionStarted = false;
                SceneManagerScript.Instance.PlayGame();
                });
    }
    void UIMoveToUp()
    {
        LeanTween.move(uiElementToUp, new Vector2(screenWidth, screenHeight * 0.327f), movingTime)
            .setEase(customCurve);
    }

    void DisableRaycast(RectTransform uiElement)
    {
        if (uiElement != null)
        {
            Graphic[] graphics = uiElement.GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                graphic.raycastTarget = false;
            }
        }
    }
}
