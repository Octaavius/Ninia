using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BossAttacks))]
public class Boss : Creature
{
    public override void ActionOnDestroy(){
        GetComponent<BossAttacks>().RemoveWarnings();
        LevelProgress.Instance.IncreaseGameLevel();
        SpawnerManager.Instance.ContinueGameAfterBoss();
        Destroy(gameObject);
    }
}
