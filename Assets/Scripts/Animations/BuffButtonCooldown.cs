using UnityEngine;
using UnityEngine.UI;

public class BuffButtonDuration : MonoBehaviour
{
    public Image durationOverlay;
    public Image cooldownOverlay;
    private float BuffDuration;
    private float cooldownDuration;

    private float timer;

    private bool wasActive; // bool to know if Buff was active in previous update to set timer for cooldown on the last Buff tick

    [SerializeField] private Buff Buff;

    void Start()
    {
        BuffDuration = Buff.BuffDuration;
        cooldownDuration = Buff.cooldownDuration;

        Buff.OnBuffActivated += UseButton;
    }

    void Update()
    {
        if(wasActive)
            UpdateDurationOverlay();
        if(!wasActive)
            UpdateCooldownOverlay();
    }

    void UpdateDurationOverlay(){
        if (!Buff.isActive) {
            timer = (wasActive) ? cooldownDuration : 0;
            wasActive = false;
            durationOverlay.fillAmount = 0;
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0) timer = 0;
        durationOverlay.fillAmount = timer / BuffDuration;
    }

    void UpdateCooldownOverlay(){
        if (!Buff.isCooldown) {
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
        timer = BuffDuration;
        wasActive = true;
    }
}