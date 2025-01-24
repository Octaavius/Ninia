using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private float CooldownDuration = 2f; 
    [SerializeField] private GameObject WarningPrefab;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Vector2 CentralSpawnpoint;
    [SerializeField] private float SpawnOffset = 1.3f;
    private Coroutine AttackCoroutine = null;
    private GameObject[] Warnings = new GameObject[2]; 
    private bool AttackIsAllowed = true; 

    private List<System.Func<IEnumerator>> AttackCoroutines;

    void Start(){
        AttackCoroutines = new List<System.Func<IEnumerator>> { DefaultAttack }; // to be extended by other types of attacks
        StartAttacking();
    }

    public void StartAttacking()
    {
        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle(){
        yield return new WaitForSeconds(1f);
        while(AttackIsAllowed){
            int randomIndex = Random.Range(0, AttackCoroutines.Count);
            AttackCoroutine = StartCoroutine(AttackCoroutines[randomIndex]());
            yield return AttackCoroutine;
            yield return new WaitForSeconds(CooldownDuration);
        }
    }

    IEnumerator DefaultAttack(){
        int safePosition = Random.Range(0, 3);
        ShowWarnings(safePosition);
        yield return new WaitForSeconds(1f);
        RemoveWarnings();
        SpawnProjectiles(safePosition);
    }

    public void StopAttacking(){
        if(AttackCoroutine == null) return;
        
        StopCoroutine(AttackCoroutine);
        AttackCoroutine = null;
        AttackIsAllowed = false;
    }

    void ShowWarnings(int safePosition){
        switch (safePosition) {
            case 0:
                Warnings[0] = Instantiate(WarningPrefab, new Vector2 (CentralSpawnpoint.x - SpawnOffset, CentralSpawnpoint.y), Quaternion.identity);
                Warnings[1] = Instantiate(WarningPrefab, CentralSpawnpoint, Quaternion.identity);
                break;
            case 1:
                Warnings[0] = Instantiate(WarningPrefab, new Vector2 (CentralSpawnpoint.x - SpawnOffset, CentralSpawnpoint.y), Quaternion.identity);
                Warnings[1] = Instantiate(WarningPrefab, new Vector2 (CentralSpawnpoint.x + SpawnOffset, CentralSpawnpoint.y), Quaternion.identity);
                break;
            case 2:
                Warnings[0] = Instantiate(WarningPrefab, new Vector2 (CentralSpawnpoint.x + SpawnOffset, CentralSpawnpoint.y), Quaternion.identity);
                Warnings[1] = Instantiate(WarningPrefab, CentralSpawnpoint, Quaternion.identity);
                break;
        }
    }

    public void RemoveWarnings(){
        if(Warnings[0] != null)
            Destroy(Warnings[0]);
        if(Warnings[1] != null)
            Destroy(Warnings[1]);
    }

    void SpawnProjectiles(int safePosition){
        GameObject firstProjectile = null;
        GameObject secondProjectile = null;
        switch (safePosition) {
            case 0:
                firstProjectile = Instantiate(ProjectilePrefab, new Vector2 (CentralSpawnpoint.x - SpawnOffset, CentralSpawnpoint.y), Quaternion.Euler(0, 0, 180));
                secondProjectile = Instantiate(ProjectilePrefab, CentralSpawnpoint, Quaternion.Euler(0, 0, 180));
                break;
            case 1:
                firstProjectile = Instantiate(ProjectilePrefab, new Vector2 (CentralSpawnpoint.x - SpawnOffset, CentralSpawnpoint.y), Quaternion.Euler(0, 0, 180));
                secondProjectile = Instantiate(ProjectilePrefab, new Vector2 (CentralSpawnpoint.x + SpawnOffset, CentralSpawnpoint.y), Quaternion.Euler(0, 0, 180));
                break;
            case 2:
                firstProjectile = Instantiate(ProjectilePrefab, new Vector2 (CentralSpawnpoint.x + SpawnOffset, CentralSpawnpoint.y), Quaternion.Euler(0, 0, 180));
                secondProjectile = Instantiate(ProjectilePrefab, CentralSpawnpoint, Quaternion.Euler(0, 0, 180));
                break;
        }
        ProjectileManager.Instance.AddNewProjectile(firstProjectile);
        ProjectileManager.Instance.AddNewProjectile(secondProjectile);
    }
}
