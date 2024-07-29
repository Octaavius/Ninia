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
        StopEffect();
        isActive = true;
        effectCoroutine = StartCoroutine(EffectCoroutine());
    }

    public void StopEffect(){
        if (effectCoroutine != null){
            StopCoroutine(effectCoroutine);
        }
        DisactivateEffect();
        isActive = false;
    }

    private IEnumerator EffectCoroutine() 
    {
        ActivateEffect();
        OnEffectActivated?.Invoke();
        yield return new WaitForSeconds(effectDuration);
        isActive = false;
    }
}