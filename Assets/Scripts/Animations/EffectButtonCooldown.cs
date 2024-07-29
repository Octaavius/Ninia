using UnityEngine;
using UnityEngine.UI;

public class EffectButtonCooldown : MonoBehaviour
{
    public Image cooldownOverlay;
    private float cooldownTime;

    private bool isCooldown = false;
    private float cooldownTimer;

    private Effect effect;

    void Start()
    {
        effect = GetComponent<Effect>();
        cooldownTime = effect.effectDuration;

        effect.OnEffectActivated += UseButton;
    }

    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0;
                isCooldown = false;
            }
            cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseButton()
    {
        // if (!isCooldown)
        // {
            isCooldown = true;
            cooldownTimer = cooldownTime;
        //}
    }
}