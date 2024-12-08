using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance {get; private set;}
    
    public delegate void DifficultyChanged(int newDifficulty);
    public event DifficultyChanged OnDifficultyChanged;

    private int _currentDifficulty = 0;
    private int _nextDifficultyScore = 200;

    void Awake()
    {
        SpawnerManager.Instance.CurrentDifficulty = _currentDifficulty;
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
        if (score >= _nextDifficultyScore)
        {
            IncreaseGameLevel();
        }
    }

    public void IncreaseGameLevel(){
        _currentDifficulty++;
        //_nextDifficultyScore = _nextDifficultyScore + 100 * _currentDifficulty;
        _nextDifficultyScore += 200;
        OnDifficultyChanged?.Invoke(_currentDifficulty);
    }

    public void ResetLevelProgress()
    {
        _currentDifficulty = 0;
        _nextDifficultyScore = 200;
        OnDifficultyChanged?.Invoke(_currentDifficulty);
    }
}