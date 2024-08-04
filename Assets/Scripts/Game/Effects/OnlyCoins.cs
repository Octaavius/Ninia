using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCoins : Effect
{
    private Coroutine scoreCoroutine;

    public int PassiveScore = 2;

    protected override void ActivateEffect(){
        StopPassiveScoreAdding();
        GameManager.Instance.spawnOnlyCoins = true;
        StartPassiveScoreAdding();
    }
    protected override void DisactivateEffect(){
        GameManager.Instance.spawnOnlyCoins = false;
        StopPassiveScoreAdding();
    }

    private void StartPassiveScoreAdding(){
        scoreCoroutine = StartCoroutine(PassiveScoreAdding());
    }

    private void StopPassiveScoreAdding(){
        if (scoreCoroutine != null){
            StopCoroutine(scoreCoroutine);
            scoreCoroutine = null;
        }
    }

    private IEnumerator PassiveScoreAdding(){
        while (true){
            GameManager.Instance.AddToScore(PassiveScore);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
