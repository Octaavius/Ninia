using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public float effectDuration = 5f;
    public float cooldownDuration = 3f;
    private Coroutine effectCoroutine;
    private Coroutine cooldownCoroutine;
    [HideInInspector] public bool isActive = false;
    [HideInInspector] public bool isCooldown = false;

    public delegate void EffectActivatedHandler();
    public event EffectActivatedHandler OnEffectActivated;

    protected abstract void ActivateEffect();
    protected abstract void DeactivateEffect();

    public void StartEffect()
    {
        if (isCooldown || effectCoroutine != null) return;
        // if (effectCoroutine != null){
        //     StopCoroutine(effectCoroutine);
        // }
        isActive = true;
        effectCoroutine = StartCoroutine(EffectCoroutine());
    }

    public void StopEffect(){
        if(effectCoroutine == null) return;
        StopCoroutine(effectCoroutine);
        DeactivateEffect();
        isActive = false;
        effectCoroutine = null;

        StartCooldown();
    }

    private IEnumerator EffectCoroutine() 
    {
        ActivateEffect();
        OnEffectActivated?.Invoke();
        yield return new WaitForSeconds(effectDuration);
        DeactivateEffect();
        isActive = false;
        effectCoroutine = null;

        StartCooldown();
    }

    private void StartCooldown()
    {
        if (cooldownCoroutine != null) return;
        isCooldown = true;
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    public void ResetCooldown(){
        isCooldown = false;
        cooldownCoroutine = null;  // Reset cooldown
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        cooldownCoroutine = null;  // Reset cooldown
    }
}