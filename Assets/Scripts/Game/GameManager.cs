using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int Score = 0;
    private int Coins = 0;
    private int Gems = 0;
    public static bool GameIsPaused;
    public GameObject EndMenuUI;
    public TMP_Text EndUiScoreText;
    public AudioManager AM;
    public MenuController menuController;

    public Player player;
    public Health HealthScript; 
    public ObstacleManager obstacleManager;

    void Awake(){
        GameIsPaused = false;
    }
    public void EndGame() {
        Time.timeScale = 0f;
        obstacleManager.DestroyAllObstacles();
        if (menuController != null)
        {
            menuController.ShowEndGameMenu(Score);
        }
        else
        {
            Debug.LogError("MenuController reference not set in GameManager.");
        }
        UpdateStats();
    }

    public void RestartGame(){
        Time.timeScale = 1f;
        EndMenuUI.SetActive(false);
        ResetNumbers();
        HealthScript.InitializeHearts();
    }

    void UpdateStats(){
        if(Score > player.BestScore)
            player.BestScore = Score;
        player.AddMoney(Coins, Gems);
        player.SaveData();
    }

    void ResetNumbers(){
        Score = 0;
        Coins = 0;
        Gems = 0;
    }
    public void AddToScore(int addAmount) {
        Score += addAmount;
    }
    public void AddToCoins(int addAmount) {
        Coins += addAmount;
    }
    public void AddToGems(int addAmount) {
        Gems += addAmount;
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
