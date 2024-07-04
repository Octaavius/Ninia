using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int s_Score = 0;
    public static int s_Coins = 0;
    public static int s_Gems = 0;

    public static bool GameIsPaused;
    [SerializeField]
    private GameObject PauseMenuUI;





    void Awake(){
        GameIsPaused = false;
    }
    public void Pause() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;    
    }

    public void EndGame() {
        
    }



    public void AddToScore(int addAmount) {
        s_Score += addAmount;
    }
    public void AddToCoins(int addAmount) {
        s_Coins += addAmount;
    }
    public void AddToGems(int addAmount) {
        s_Gems += addAmount;
    }
}
