using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public delegate void DifficultyChanged(int newDifficulty);
    public static event DifficultyChanged OnDifficultyChanged;

    private int currentDifficulty = 0;
    private int nextDifficultyScore = 100;

    void Update()
    {
        UpdateGameLevel(GameManager.Instance.GetScore());
    }

    private void UpdateGameLevel(int score)
    {
        Debug.Log($"Current score: {score}");
        if (score >= nextDifficultyScore)
        {
            currentDifficulty++;
            nextDifficultyScore = nextDifficultyScore + 200 * currentDifficulty;
            Debug.Log($"Difficulty increased to {currentDifficulty}");
            OnDifficultyChanged?.Invoke(currentDifficulty);
        }
    }
}