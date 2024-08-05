using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeChangingButton : MonoBehaviour
{
    public List<string> modesList; // List of modes
    private int currentMode = 0; // Current mode index
    
    public TMP_Text modeText; // Text object to display current mode
    public TMP_Text nextModeText; // Text object to display next mode
    public float moveDuration = 0.5f; // Duration of the move animation

    private bool isAnimating = false; // Flag to indicate if animation is in progress
    private bool isChangeRequested = false; // Flag to queue a mode change request

    // Start is called before the first frame update
    void Start()
    {
        modeText.text = modesList[currentMode];
    }

    public void ChangeMode()
    {
        if (isAnimating)
        {
            isChangeRequested = true;
            return;
        }

        isAnimating = true;
        int nextMode = getNextMode();
        
        string nextModeName = modesList[nextMode];
        
        nextModeText.text = nextModeName;
        SceneManagerMenu.Instance.sceneName = nextModeName;

        // Animate the current mode text moving down
        LeanTween.moveY(modeText.rectTransform, -modeText.rectTransform.rect.height, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // Animate the next mode text moving down to the original position of the current mode text
        LeanTween.moveY(nextModeText.rectTransform, 0, moveDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
        {
            // Reset positions after animation
            modeText.rectTransform.anchoredPosition = Vector2.zero;
            nextModeText.rectTransform.anchoredPosition = new Vector2(0, modeText.rectTransform.rect.height);

            // Update the mode text
            modeText.text = modesList[nextMode];

            isAnimating = false;

            if (isChangeRequested)
            {
                isChangeRequested = false;
                ChangeMode();
            }
        });

        currentMode = nextMode;
    }

    private int getNextMode()
    {
        currentMode++;
        if (currentMode == modesList.Count) currentMode = 0;
        return currentMode;
    }
}
