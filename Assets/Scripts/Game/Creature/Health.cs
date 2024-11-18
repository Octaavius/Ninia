using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{    
    [Header("Health Bar Settings")]
    [SerializeField] private Transform LeftHpBarBdPos;
    [SerializeField] private GameObject HpBarFilling;
    [SerializeField] private TMP_Text HpText;
    
    [Header("Shield")]
    [SerializeField] private Shield Shield;
    
    [Header("Health Settings")]
    [Min(0f)]
    public float MaxHp = 100;
    public float HpRegenPerSec = 0; 
    public float HealMult = 1;
    
    [HideInInspector] public float CurrHp = 0;
    private bool ShowNumbers = false;

    private float Timer;
    private float IntervBetwPasHeal = 0.5f;

    void Update() {
        Timer += Time.deltaTime;

        if (Timer >= IntervBetwPasHeal) {
            Heal(HpRegenPerSec * IntervBetwPasHeal);  
            Timer -= IntervBetwPasHeal;   
        }
    }

    public void InitializeHealth()
    {
        CurrHp = MaxHp;
        UpdateHealthBar();
    }

    private void UpdateHealthBar(){
        float fillAmount = CurrHp / MaxHp;

        if (HpBarFilling.GetComponent<SpriteRenderer>() != null) 
        {
            HpBarFilling.transform.localScale = new Vector3(fillAmount, HpBarFilling.transform.localScale.y, 1f);
            HpBarFilling.transform.localPosition = new Vector2(LeftHpBarBdPos.localPosition.x * (1f - fillAmount), HpBarFilling.transform.localPosition.y);
        } 
        else if (HpBarFilling.GetComponent<Image>() != null) 
        {
            HpBarFilling.GetComponent<Image>().fillAmount = fillAmount;
        } 

        HpText.text = ShowNumbers ? $"{CurrHp}" : "";
    }

    public void Heal(float healAmount)
    {
        CurrHp += healAmount;
        
        if(CurrHp >= MaxHp){
            CurrHp = MaxHp;
        }
        UpdateHealthBar();
    }

    public bool RemoveHealth(float removeAmount)
    {
        if(Shield != null && Shield.isActive){
            Shield.DecreaseShieldHp();
            return false;
        }

        CurrHp -= removeAmount;        

        if (CurrHp <= 0){
            CurrHp = 0;
            return true;
        }
        UpdateHealthBar();
        return false;
    }

    public void setMaxHealth(float newMaxHp){
        MaxHp = newMaxHp;
        UpdateHealthBar();
    }
    public void SetShowNumbers(bool show)
    {
        ShowNumbers = show;
        UpdateHealthBar();
    }
}
