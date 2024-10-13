using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCoins : Buff
{
    private Coroutine scoreCoroutine;

    public int PassiveScore = 2;
    public OnlyCoinsAnimation coinsAnimation;

    protected override void ActivateBuff(){
        StopPassiveScoreAdding();
        coinsAnimation.ActivateAnimation();
        GameManager.Instance.spawnOnlyCoins = true;
        StartPassiveScoreAdding();
    }
    protected override void DeactivateBuff(){
        GameManager.Instance.spawnOnlyCoins = false;
        StopPassiveScoreAdding();
        coinsAnimation.DeactivateAnimation();
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
