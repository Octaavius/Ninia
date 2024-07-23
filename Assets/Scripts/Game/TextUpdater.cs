using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    ///////////////////////////////////////
    public GameManager gameManager;
    ///////////////////////////////////////
    
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private TMP_Text GemsText;
    void Update() {
        ScoreText.text = gameManager.GetScore().ToString();
        CoinsText.text = gameManager.GetCoins().ToString();
        GemsText.text = gameManager.GetGems().ToString();
    }
}