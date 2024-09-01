using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{    
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TMP_Text healthText;
    private int maxHealth = 100;
    private int currentHealth = 0;
    
    [SerializeField] private Shield shield;

    private bool showNumbers = false;
    public Gradient gradient;

    public void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar(){
        healthBarImage.fillAmount = ((float)currentHealth)/ maxHealth;
        healthText.text = showNumbers ? $"{currentHealth}/{maxHealth}" : "";
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        
        if(currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    public void RemoveHealth(int removeAmount)
    {
        if(shield.isActive){
            shield.DecreaseShieldHp();
            return;
        }

        currentHealth -= removeAmount;        

        if (currentHealth <= 0){
            currentHealth = 0;
            GameManager.Instance.EndGame();
        } 
        UpdateHealthBar();
    }

    public void setMaxHealth(int newMaxHealth){
        maxHealth = newMaxHealth;
        UpdateHealthBar();
    }
    public void SetShowNumbers(bool show)
    {
        showNumbers = show;
        UpdateHealthBar();
    }

}
