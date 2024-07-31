using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int Score = 0;
    private int AddedCoins = 0;
    private int Coins = 0;
    private int Gems = 0;
    private int AddedGems = 0;

    private int ScoreMultiplier = 1;
    private int CoinsMultiplier = 1;
    
    [HideInInspector] public bool magnetEffectActivated = false;
    [HideInInspector] public bool spawnOnlyCoins;

    [HideInInspector] public bool GameIsPaused;
    [HideInInspector] public bool GameIsOver;
    
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
        InitializeGame();
    }

    private void InitializeGame() {
        GameIsOver = false;
        ResetNumbers();
        UpdateTexts();
        ninjaController.InitializeNinja();
        SpawnerManager.Instance.ResetSpawners();
        if(LevelProgress.Instance != null)
            LevelProgress.Instance.ResetLevelProgress();
    }

    public void EndGame() {
    	GameIsOver = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOverSound);
        EffectsManager.Instance.RemoveAllEffects();
        Time.timeScale = 0f;
        ProjectileManager.Instance.DestroyAllProjectiles();
        menuController.ShowEndGameMenu(Score);
        SpawnerManager.Instance.ResetSpawners();
        UpdateStats();
    }

    public void RestartGame(){
        Time.timeScale = 1f;
        InitializeGame();
    }

    void UpdateTexts() {
        ScoreText.text = Score.ToString();
        CoinsText.text = Coins.ToString();
        GemsText.text = Gems.ToString();
    }
    
    public void UpdateStats(){
        if(Score > playerInfo.BestScore)
            playerInfo.BestScore = Score;
        int coinsToAdd =  Coins - AddedCoins;
        int gemsToAdd = Gems - AddedGems;
        AddedCoins = Coins;
        AddedGems = Gems;
        playerInfo.AddMoney(coinsToAdd, gemsToAdd);
        playerInfo.SaveData();
    }

    void ResetNumbers(){
        Score = 0;
        Coins = 0;
        AddedCoins = 0;
        Gems = 0;
        AddedGems = 0;
    }
    public void SetScoreMultiplier(int newScoreMultiplier){
        ScoreMultiplier = newScoreMultiplier;
    }
    public void SetCoinsMultiplier(int newCoinsMultiplier){
        CoinsMultiplier = newCoinsMultiplier;
    }
    public void AddToScore(int addAmount) {
        Score += addAmount * ScoreMultiplier;
        UpdateTexts();
    }
    public void AddToCoins(int addAmount) {
        Coins += addAmount * CoinsMultiplier;
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
