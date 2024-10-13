using System.Collections;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public float BuffDuration = 5f;
    public float cooldownDuration = 3f;
    private Coroutine buffCoroutine;
    private Coroutine cooldownCoroutine;
    [HideInInspector] public bool isActive = false;
    [HideInInspector] public bool isCooldown = false;

    public delegate void BuffActivatedHandler();
    public event BuffActivatedHandler OnBuffActivated;

    protected abstract void ActivateBuff();
    protected abstract void DeactivateBuff();

    public void StartBuff()
    {
        if (isCooldown || buffCoroutine != null) return;
        // if (BuffCoroutine != null){
        //     StopCoroutine(BuffCoroutine);
        // }
        isActive = true;
        buffCoroutine = StartCoroutine(BuffCoroutine());
    }

    public void StopBuff(){
        if(buffCoroutine == null) return;
        StopCoroutine(buffCoroutine);
        DeactivateBuff();
        isActive = false;
        buffCoroutine = null;

        StartCooldown();
    }

    private IEnumerator BuffCoroutine() 
    {
        ActivateBuff();
        OnBuffActivated?.Invoke();
        yield return new WaitForSeconds(BuffDuration);
        DeactivateBuff();
        isActive = false;
        buffCoroutine = null;

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