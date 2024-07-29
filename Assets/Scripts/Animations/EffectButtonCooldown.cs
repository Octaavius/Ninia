using UnityEngine;
using UnityEngine.UI;

public class EffectButtonDuration : MonoBehaviour
{
    public Image durationOverlay;
    private float effectDuration;

    private float durationTimer;

    private Effect effect;

    void Start()
    {
        effect = GetComponent<Effect>();
        effectDuration = effect.effectDuration;

        effect.OnEffectActivated += UseButton;
    }

    void Update()
    {
        if (effect.isActive)
        {
            durationTimer -= Time.deltaTime;
            if (durationTimer <= 0)
            {
                durationTimer = 0;
            }
            durationOverlay.fillAmount = durationTimer / effectDuration;
        } else {
            durationTimer = 0;
            durationOverlay.fillAmount = 0;
        }
    }

    public void UseButton()
    {
        durationTimer = effectDuration;
    }
}