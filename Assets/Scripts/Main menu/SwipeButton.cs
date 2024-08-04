using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float swipeThreshold = 50f; // Threshold in pixels to consider it a swipe
    private Vector2 pointerDownPosition;
    private Vector2 pointerUpPosition;
    private bool isSwipe;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("SwipeButton must be attached to a GameObject with a Button component.");
        }
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
            // If it's not a swipe, we simulate the button click
            //button.onClick.Invoke();
            OnClick();
        } else {
            Debug.Log("it is a swipe, we ARE NOT SWIPING");
        }
    }

    public virtual void OnClick(){
        SceneManagerMenu.Instance.PlayGame();
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
