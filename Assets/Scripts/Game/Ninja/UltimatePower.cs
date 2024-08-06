using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimatePower : MonoBehaviour
{
    private ShakeAnimation shakeAnimation;

    [SerializeField] private Image ultimateBarImage; 
    private float currentUltimateCharge = 0f;
    private const float maxUltimateCharge = 1f;

    private bool ultiIsReady = false; 

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

}
