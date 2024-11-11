using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : Projectile
{
    [Header("Boss Settings")]
    [SerializeField] private int scorePrice = 12;
    private float SpawnChance;

    [SerializeField] private float maxHealth = 100f;
    //[SerializeField] private int damage = 30;
    private float currentHealth = 0f;
    
    [Header("Health Bar Settings")]
    [SerializeField] private Transform leftHealthBarBorderPosition;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarFilling;
    [SerializeField] private TMP_Text healthTextPillow;

    void Start(){
        InitializeHealth();
    }

    void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public override void UpdateHealthBar(){
        float fillAmount = currentHealth / maxHealth;
        healthBarFilling.transform.localScale = new Vector3(fillAmount, healthBarFilling.transform.localScale.y, 1f);
        healthBarFilling.transform.localPosition = new Vector2(leftHealthBarBorderPosition.localPosition.x * (1f - fillAmount), healthBarFilling.transform.localPosition.y);
        healthTextPillow.text = showNumbers ? $"{currentHealth}" : "";
        // does not work, as boss is not a projectile so it not seen in spawners.
    }

    public override void OnToggleChange() {
        healthTextPillow.text = showNumbers ? $"{currentHealth}" : "";
    }
    
    public override void ActionOnCollision(){

    }

    public override void ActionOnDestroy(){
        GetComponent<BossAttacks>().RemoveWarnings();
        Destroy(gameObject);
        GameManager.Instance.AddToScore(scorePrice);
        LevelProgress.Instance.IncreaseGameLevel();
        SpawnerManager.Instance.ContinueGameAfterBoss();
    }

    public override bool TakeDamage(float takenDamage){
        currentHealth -= takenDamage;
        UpdateHealthBar();
        if (currentHealth <= 0){
            ActionOnDestroy();
            return true;
        }  
        return false;
    }
}
