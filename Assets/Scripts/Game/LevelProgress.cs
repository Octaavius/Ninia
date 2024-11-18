using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance {get; private set;}
    
    public delegate void DifficultyChanged(int newDifficulty);
    public event DifficultyChanged OnDifficultyChanged;

    private int currentDifficulty = 0;
    private int nextDifficultyScore = 200;

    void Awake()
    {
        SpawnerManager.Instance.currentDifficulty = currentDifficulty;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        UpdateGameLevel(GameManager.Instance.GetScore());
    }
    private void UpdateGameLevel(int score)
    {
        if (score >= nextDifficultyScore)
        {
            currentDifficulty++;
            nextDifficultyScore = nextDifficultyScore + 100 * currentDifficulty;
            OnDifficultyChanged?.Invoke(currentDifficulty);
        }
    }

    public void IncreaseGameLevel(){
        currentDifficulty++;
        //nextDifficultyScore = nextDifficultyScore + 100 * currentDifficulty;
        nextDifficultyScore = nextDifficultyScore + 200;
        OnDifficultyChanged?.Invoke(currentDifficulty);
    }

    public void ResetLevelProgress()
    {
        currentDifficulty = 0;
        nextDifficultyScore = 100;
        OnDifficultyChanged?.Invoke(currentDifficulty);
    }
}