using UnityEngine;
using UnityEngine.UI;

public class EffectButtonDuration : MonoBehaviour
{
    public Image durationOverlay;
    public Image cooldownOverlay;
    private float effectDuration;
    private float cooldownDuration;

    private float timer;

    private bool wasActive; // bool to know if effect was active in previous update to set timer for cooldown on the last effect tick

    [SerializeField] private Effect effect;

    void Start()
    {
        effectDuration = effect.effectDuration;
        cooldownDuration = effect.cooldownDuration;

        effect.OnEffectActivated += UseButton;
    }

    void Update()
    {
        if(wasActive)
            UpdateDurationOverlay();
        if(!wasActive)
            UpdateCooldownOverlay();
    }

    void UpdateDurationOverlay(){
        if (!effect.isActive) {
            timer = (wasActive) ? cooldownDuration : 0;
            wasActive = false;
            durationOverlay.fillAmount = 0;
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0) timer = 0;
        durationOverlay.fillAmount = timer / effectDuration;
    }

    void UpdateCooldownOverlay(){
        if (!effect.isCooldown) {
            timer = 0;
            cooldownOverlay.fillAmount = 0;
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0) timer = 0;
        cooldownOverlay.fillAmount = timer / cooldownDuration;
    }

    public void UseButton()
    {
        timer = effectDuration;
        wasActive = true;
    }
}