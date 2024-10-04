using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pillow : Projectile
{
    [Header("Pillow Settings")]
    [SerializeField] private int scorePrice = 12;
    private float SpawnChance;

    private float maxHealth = 100f;
    private float currentHealth = 0f;
    [SerializeField] private Transform leftHealthBarBorderPosition;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarFilling;
    [SerializeField] private TMP_Text healthTextPillow;

    void Start(){
        InitializeHealth();
    }

    void InitializeHealth()
    {
        healthBar.transform.position = transform.position + new Vector3(0f, .55f, 0f);
        healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealthBar(){
        float fillAmount = currentHealth / maxHealth;
        healthBarFilling.transform.localScale = new Vector3(fillAmount, healthBarFilling.transform.localScale.y, 1f);
        healthBarFilling.transform.localPosition = new Vector2(leftHealthBarBorderPosition.localPosition.x * (1f - fillAmount), healthBarFilling.transform.localPosition.y);
        healthTextPillow.text = showNumbers ? $"{currentHealth}" : "";
    }

    public override float GetSpawnChance() => SpawnChance;
    public override void OnToggleChange() {
        healthTextPillow.text = showNumbers ? $"{currentHealth}" : "";
    }
    
    public override void ActionOnCollision(){
	    AudioManager.Instance.PlaySFX(AudioManager.Instance.collisionSound);
        GameManager.Instance.ninjaController.healthScript.RemoveHealth(30);
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        GameManager.Instance.AddToScore(scorePrice);
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage){
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0){
            ActionOnDestroy();
            alive = false;
        }  
    }
}

