using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance {get; private set;}
    
    public delegate void DifficultyChanged(int newDifficulty);
    public event DifficultyChanged OnDifficultyChanged;

    [HideInInspector] public int currentDifficulty = 0;
    private int nextDifficultyScore = 100;

    void Awake()
    {
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

    public void ResetLevelProgress()
    {
        currentDifficulty = 0;
        nextDifficultyScore = 100;
        OnDifficultyChanged?.Invoke(currentDifficulty);
    }
}