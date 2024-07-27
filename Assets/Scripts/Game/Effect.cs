using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int effectDuration = 5;
    private Coroutine effectCoroutine;

    public abstract void ActivateEffect();
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
        Debug.Log("Effect Activated");

        yield return new WaitForSeconds(effectDuration);
        DisactivateEffect();
        Debug.Log("Effect Disactivated");
    }

    public void ResetEffect()
    {
        StartEffect();
    }
}
