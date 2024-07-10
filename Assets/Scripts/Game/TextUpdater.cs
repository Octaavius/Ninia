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
    public GameManager gameManager;
    void Update() {
        ScoreText.text = gameManager.GetScore().ToString();
        CoinsText.text = gameManager.GetCoins().ToString();
        GemsText.text = gameManager.GetGems().ToString();
    }
}