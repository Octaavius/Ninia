using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UltimatePower : MonoBehaviour
{
    private ShakeAnimation shakeAnimation;

    [SerializeField] private Image ultimateBarImage;
    [SerializeField] private TMP_Text ultimateText;
    private float currentUltimateCharge = 0f;
    private const float maxUltimateCharge = 1f;

    private bool ultiIsReady = false;
    private bool showNumbers = false;

    [SerializeField] private Color readyColor = Color.red;
    [SerializeField] private Color defaultColor = Color.green;

    // [Header("Camera to Shake")]
    // public Transform cameraTransform;

    void Start() {
	    shakeAnimation = GetComponent<ShakeAnimation>();
    }

    void Update(){
        PassiveUltimateChargeDecrease();
        UpdateUltimateBar();
        ultimateText.text = showNumbers ? $"{Mathf.Round(currentUltimateCharge * 100)}%" : "";
    }

    public void TryActivate(){
        if(!ultiIsReady) return;
        
        shakeAnimation.TriggerShake();
        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.boomSound);

        ProjectileManager.Instance.DestroyAllProjectiles();
        
        ResetUltimatePower();
    } 

    public void AddCharge(){
        currentUltimateCharge += 1f / 20;
        if(currentUltimateCharge >= maxUltimateCharge){
            ultiIsReady = true;
            currentUltimateCharge = maxUltimateCharge;
        }
    }

    private void PassiveUltimateChargeDecrease(){
        if(ultiIsReady) return;
        if(currentUltimateCharge <= 0) return;
        currentUltimateCharge -= (1f / 40f) * Time.deltaTime;
    }

    private void UpdateUltimateBar(){
        ultimateBarImage.fillAmount = currentUltimateCharge / maxUltimateCharge;

        if(ultiIsReady){
            ultimateBarImage.color = readyColor;
        } else {
            ultimateBarImage.color = defaultColor;
        }
    }
    public void ResetUltimatePower(){
        currentUltimateCharge = 0f;
        ultiIsReady = false;
    }
    public void SetShowNumbers(bool show)
    {
        showNumbers = show;
        ultimateText.text = showNumbers ? $"{Mathf.Round(currentUltimateCharge * 100)}%" : "";
    }
}
