using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int effectDuration = 5;
    private Coroutine effectCoroutine;
    [HideInInspector] public bool isActive = false;

    public delegate void EffectActivatedHandler();
    public event EffectActivatedHandler OnEffectActivated;

    protected abstract void ActivateEffect();
    protected abstract void DisactivateEffect();

    public void StartEffect()
    {
        if (effectCoroutine != null){
            StopCoroutine(effectCoroutine);
        }
        isActive = true;
        effectCoroutine = StartCoroutine(EffectCoroutine());
    }

    public void StopEffect(){
        if(effectCoroutine == null) return;
        StopCoroutine(effectCoroutine);
        DisactivateEffect();
        isActive = false;
    }

    private IEnumerator EffectCoroutine() 
    {
        ActivateEffect();
        OnEffectActivated?.Invoke();
        yield return new WaitForSeconds(effectDuration);
        DisactivateEffect();
        isActive = false;
    }
}