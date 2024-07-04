using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    private enum GameDifficulty {
        Easy, 
        Medium,
        Hard,
        Challenging,
        Impossible
    }

    void Update() {
        UpdateGameLevel(GameManager.GetScore());
    }    

    private void UpdateGameLevel(int score){
        GameDifficulty currentDifficulty = GetGameDifficulty(score);
        switch(currentDifficulty) {
            
        }
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
