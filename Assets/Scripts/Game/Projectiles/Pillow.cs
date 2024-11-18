using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pillow : Projectile
{
    [Header("Pillow Settings")]
    [SerializeField] private int scorePrice = 12;
    private float SpawnChance;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int damage = 30;
    private float currentHealth = 0f;
    
    [Header("Health Bar Settings")]
    [SerializeField] private Transform leftHealthBarBorderPosition;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarFilling;
    [SerializeField] private TMP_Text healthTextPillow;
    [SerializeField] private float healthBarYPosition = .55f;

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
        
        healthBar.transform.SetPositionAndRotation(
            new Vector3(transform.position.x, transform.position.y + healthBarYPosition, transform.position.z),
            Quaternion.identity
        );

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
        NinjaController.Instance.HpScr.RemoveHealth(damage);
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        GameManager.Instance.AddToScore(scorePrice);
        Destroy(gameObject);
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

