using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pillow : Projectile
{
    [SerializeField] private int scorePrice = 12;
    private float SpawnChance;

    private float maxHealth = 100f;
    private float currentHealth = 0f;
    [SerializeField] private GameObject healthBarImage;
    [SerializeField] private TMP_Text healthTextPillow;

    void Start(){
        InitializeHealth();
    }

    void InitializeHealth()
    {
        healthBarImage.transform.position = transform.position + new Vector3(0f, 1f, 0f);
        healthBarImage.transform.rotation = Quaternion.Euler(0, 0, 0);
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealthBar(){
        float fillAmount = currentHealth / maxHealth;
        float currentXScale = healthBarImage.transform.localScale.x;
        healthBarImage.transform.localScale = new Vector3(currentXScale * fillAmount, healthBarImage.transform.localScale.y, 1f);
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

    public override string ActionOnHit(){
        currentHealth -= 51f;
        UpdateHealthBar();
        if (currentHealth <= 0){
            ActionOnDestroy();
            return "destroyed";
        }  
        return "not destroyed";
    }
}

