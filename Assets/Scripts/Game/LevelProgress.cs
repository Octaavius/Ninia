using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    ///////////////////////////////////////
    public GameManager gameManager;
    ///////////////////////////////////////
    
    [HideInInspector] public static GameDifficulty currentGameDifficulty = GameDifficulty.Easy;
    [HideInInspector] public enum GameDifficulty {
        Easy, 
        Medium,
        Hard,
        Challenging,
        Impossible
    }

    void Update() {
        UpdateGameLevel(gameManager.GetScore());
    }    

    private void UpdateGameLevel(int score){
        GameDifficulty updatedDifficulty = GetGameDifficulty(score);
        if(currentGameDifficulty == updatedDifficulty){
            return;
        }
        currentGameDifficulty = updatedDifficulty;
    }

    private GameDifficulty GetGameDifficulty(int score) {
        if(score < 100) {
            return GameDifficulty.Easy;
        } else if (score < 300) {
            return GameDifficulty.Medium;
        } else if (score < 600) {
            return GameDifficulty.Hard;
        } else if (score < 1000) {
            return GameDifficulty.Challenging;
        } else {
            return GameDifficulty.Impossible;
        }
    }

}
