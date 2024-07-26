using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int Score = 0;
    private int Coins = 0;
    private int Gems = 0;

    public static bool GameIsPaused;
    
    [Header("EndMenu")]
    public GameObject EndMenuUI;
    public TMP_Text EndUiScoreText;
    public AudioManager am;

    public PlayerInfo playerInfo;
    public Health HealthScript; 
    public ProjectileManager projectileManager;

    [Header("Texts to update")]
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private TMP_Text GemsText;

    void Awake(){
        GameIsPaused = false;
    }

    public void EndGame() {
        Time.timeScale = 0f;
        projectileManager.DestroyAllProjectiles();
        EndUiScoreText.text = Score.ToString();
        EndMenuUI.SetActive(true);
        am.PlaySFX(am.gameOverSound);
        UpdateStats();
    }

    public void RestartGame(){
        Time.timeScale = 1f;
        EndMenuUI.SetActive(false);
        ResetNumbers();
        HealthScript.InitializeHearts();
    }

    void UpdateTexts() {
        ScoreText.text = Score.ToString();
        CoinsText.text = Coins.ToString();
        GemsText.text = Gems.ToString();
    }
    
    void UpdateStats(){
        if(Score > playerInfo.BestScore)
            playerInfo.BestScore = Score;
        playerInfo.AddMoney(Coins, Gems);
        playerInfo.SaveData();
    }

    void ResetNumbers(){
        Score = 0;
        Coins = 0;
        Gems = 0;
    }
    public void AddToScore(int addAmount) {
        Score += addAmount;
        UpdateTexts();
    }
    public void AddToCoins(int addAmount) {
        Coins += addAmount;
        UpdateTexts();
    }
    public void AddToGems(int addAmount) {
        Gems += addAmount;
        UpdateTexts();
    }
    public int GetScore() {
        return Score;
    }
    public int GetCoins() {
        return Coins;
    }
    public int GetGems() {
        return Gems;
    }
}
