using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{    
    [Header("Health Bar Settings")]
    [SerializeField] private Transform _leftHpBarBdPos;
    [SerializeField] private GameObject _hpBarFilling;
    [SerializeField] private TMP_Text _hpText;
    
    [Header("Shield")]
    [SerializeField] private Shield _shield;
    
    [Header("Health Settings")]
    [Min(0f)]
    public float MaxHp = 100;
    public float HpRegenPerSec = 0; 
    public float HealMult = 1;
    
    [HideInInspector] public float CurrHp = 0;
    private bool ShowNumbers = false;

    private float _timer;
    private float _intervBetwPasHeal = 0.5f;

    void Update() {
        _timer += Time.deltaTime;

        if (_timer >= _intervBetwPasHeal) {
            Heal(HpRegenPerSec * _intervBetwPasHeal);  
            _timer -= _intervBetwPasHeal;   
        }
    }

    public void InitializeHealth()
    {
        CurrHp = MaxHp;
        UpdateHealthBar();
    }

    private void UpdateHealthBar(){
        float fillAmount = CurrHp / MaxHp;

        if (_hpBarFilling.GetComponent<SpriteRenderer>() != null) 
        {
            _hpBarFilling.transform.localScale = new Vector3(fillAmount, _hpBarFilling.transform.localScale.y, 1f);
            _hpBarFilling.transform.localPosition = new Vector2(_leftHpBarBdPos.localPosition.x * (1f - fillAmount), _hpBarFilling.transform.localPosition.y);
        } 
        else if (_hpBarFilling.GetComponent<Image>() != null) 
        {
            _hpBarFilling.GetComponent<Image>().fillAmount = fillAmount;
        } 

        _hpText.text = ShowNumbers ? $"{CurrHp}" : "";
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
        if(_shield != null && _shield.isActive){
            _shield.DecreaseShieldHp();
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
