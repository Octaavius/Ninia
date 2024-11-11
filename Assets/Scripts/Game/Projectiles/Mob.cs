using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mob : Projectile
{
    [Header("Mob Settings")]
    [SerializeField] private int scorePrice = 12;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int damage = 30;
    [SerializeField] private GameObject mobSprite;
    private float SpawnChance;
    private float currentHealth = 0f;
    
    [Header("Health Bar Settings")]
    [SerializeField] private Transform leftHealthBarBorderPosition;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarFilling;
    [SerializeField] private TMP_Text healthTextPillow;
    [SerializeField] private float healthBarYPosition = .55f;

    void Start(){
        InitializeHealth();
        SetSpriteScale();
    }

    void SetSpriteScale() {
        if (transform) {
            // Set the global rotation of the sprite to 0 (no rotation in world space)
            mobSprite.transform.rotation = Quaternion.identity;

            // Get the global rotation angle of the main object
            float rotationAngle = transform.eulerAngles.z;

            // Flip the scale based on specific angles in the global rotation
            Debug.Log(rotationAngle);
            if (rotationAngle == 0 || rotationAngle == 90) {
                mobSprite.transform.localScale = new Vector3(-mobSprite.transform.localScale.x, mobSprite.transform.localScale.y, 1); 
            }
        }
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
        GameManager.Instance.ninjaController.healthScript.RemoveHealth(damage);
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

