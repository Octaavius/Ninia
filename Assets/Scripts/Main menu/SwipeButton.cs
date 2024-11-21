using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwipeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float swipeThreshold = 50f; // Threshold in pixels to consider it a swipe
    private Vector2 pointerDownPosition;
    private Vector2 pointerUpPosition;
    private bool isSwipe;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownPosition = eventData.position;
        isSwipe = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUpPosition = eventData.position;
        CheckForSwipe();
        
        if (!isSwipe)
        {
            button.onClick.Invoke();
        }
    }

    private void CheckForSwipe()
    {
        float distance = Vector2.Distance(pointerDownPosition, pointerUpPosition);
        if (distance > swipeThreshold)
        {
            isSwipe = true;
        }
    }
}
