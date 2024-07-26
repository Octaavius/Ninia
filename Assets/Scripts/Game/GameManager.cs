using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int Score = 0;
    private int Coins = 0;
    private int Gems = 0;

    [HideInInspector]public bool GameIsPaused;
    
    private MenuController menuController;
    public PlayerInfo playerInfo;
    public NinjaController ninjaController; 

    [Header("Texts to update")]
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private TMP_Text GemsText;

    public static GameManager Instance { get; private set; }

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        menuController = GetComponent<MenuController>();
        GameIsPaused = false;
    }

    public void EndGame() {
        Time.timeScale = 0f;
        ProjectileManager.Instance.DestroyAllProjectiles();
        menuController.ShowEndGameMenu(Score);
        UpdateStats();
    }

    public void RestartGame(){
        Time.timeScale = 1f;
        ResetNumbers();
        UpdateTexts();
        ninjaController.InitializeNinja();
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
