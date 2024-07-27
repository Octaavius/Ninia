using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int effectDuration = 5;
    private Coroutine effectCoroutine;

    // Define an event to notify when the effect is activated
    public delegate void EffectActivatedHandler();
    public event EffectActivatedHandler OnEffectActivated;

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
        
        // Trigger the event when the effect is activated
        OnEffectActivated?.Invoke();

        yield return new WaitForSeconds(effectDuration);
        DisactivateEffect();
        Debug.Log("Effect Disactivated");
    }
}