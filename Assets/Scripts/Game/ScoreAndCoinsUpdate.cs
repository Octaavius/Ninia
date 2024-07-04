using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    // Array to hold references to Text components
    public Text[] textElements;

    // Array to hold new texts for each Text component
    public string[] newTexts;

    void Start()
    {
        // Ensure that the arrays are of the same length
        if (textElements.Length != newTexts.Length)
        {
            Debug.LogError("Text elements array and new texts array must be of the same length.");
            return;
        }

        // Loop through each text element and change its text
        for (int i = 0; i < textElements.Length; i++)
        {
            textElements[i].text = newTexts[i];
        }
    }
}