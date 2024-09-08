using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;

public class GameManager : MonoBehaviour
{
    private int Score = 0;
    private int AddedCoins = 0;
    private int Coins = 0;
    private int Gems = 0;
    private int AddedGems = 0;
    private int OnlyCoinsScore = 0;

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
    [SerializeField] private TMP_Text OnlyCoinsText;

    [Header("Spawners")]
    public List<Spawner> spawners;

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
        SpawnerManager.Instance.StartSpawning(spawners.ToArray());
        if (LevelProgress.Instance != null)
            LevelProgress.Instance.ResetLevelProgress();
        bool showNumbers = PlayerPrefs.GetInt("BetterUI", 1) == 1;
        UpdateBetterUiState(showNumbers);
    }

    public void EndGame() {
    	GameIsOver = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOverSound);
        EffectsManager.Instance.RemoveAllEffects();
        Time.timeScale = 0f;
        ProjectileManager.Instance.RemoveAllProjectiles();
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
        OnlyCoinsText.text = OnlyCoinsScore.ToString();
        //menuController.betterUiToggle.isOn = PlayerPrefs.GetInt("BetterUI", 1) == 1;
        //UpdateBetterUiState(menuController.betterUiToggle.isOn);
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
    public void AddToCoins(int addAmount)
    {
        Coins += addAmount * CoinsMultiplier;
        if (spawnOnlyCoins) {
            OnlyCoinsScore += addAmount * CoinsMultiplier;
        } else {
            Invoke("ResetOnlyCoinsScore", 0.7f);
        }
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
    public void UpdateBetterUiState(bool showNumbers)
    {
        ninjaController.healthScript.SetShowNumbers(showNumbers);
        ninjaController.ulti.SetShowNumbers(showNumbers);
        ProjectileManager.Instance.SetShowNumbers(showNumbers);
    }

    private void ResetOnlyCoinsScore()
    {
      OnlyCoinsScore = 0;
        UpdateTexts();
    }
}
