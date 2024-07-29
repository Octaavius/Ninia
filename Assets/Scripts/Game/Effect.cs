using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int effectDuration = 5;
    private Coroutine effectCoroutine;

    public delegate void EffectActivatedHandler();
    public event EffectActivatedHandler OnEffectActivated;

    protected abstract void ActivateEffect();
    protected abstract void DisactivateEffect();

    public void StartEffect()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
        }
        effectCoroutine = StartCoroutine(EffectCoroutine());
    }

    private IEnumerator EffectCoroutine() 
    {
        ActivateEffect();

        OnEffectActivated?.Invoke();

        yield return new WaitForSeconds(effectDuration);
        DisactivateEffect();
    }
}