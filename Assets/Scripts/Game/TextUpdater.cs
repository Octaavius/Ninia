using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text CoinsText;
    [SerializeField]
    private TMP_Text GemsText;

    void Start()
    {
        // // Ensure that the arrays are of the same length
        // if (textElements.Length != newTexts.Length)
        // {
        //     Debug.LogError("Text elements array and new texts array must be of the same length.");
        //     return;
        // }

        // // Loop through each text element and change its text
        // for (int i = 0; i < textElements.Length; i++)
        // {
        //     textElements[i].text = newTexts[i];
        // }
    }

    void Update() {
        ScoreText.text = GameManager.s_Score.ToString();
        CoinsText.text = GameManager.s_Coins.ToString();
        GemsText.text = GameManager.s_Gems.ToString();
    }
}