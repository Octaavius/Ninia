using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Buff
{
    public GameObject ShieldObject;
    public float fadeDuration = 1f;

    private int ShieldHp = 3;
    private int ShieldCurrentHp = 0;
    private SpriteRenderer ShieldSpriteRenderer;
    private float currentAlpha = 0f;

    void Start(){
        ShieldSpriteRenderer = ShieldObject.GetComponent<SpriteRenderer>();
    }

    protected override void ActivateBuff()
    {
        ShieldObject.SetActive(true);
        ShieldCurrentHp = ShieldHp;
        currentAlpha = 1f;
        FadeTo(1f, fadeDuration);
    }

    protected override void DeactivateBuff()
    {
        FadeTo(0f, fadeDuration, () => ShieldObject.SetActive(false));
    }

    private void FadeTo(float targetAlpha, float duration, System.Action onComplete = null)
    {
        float startAlpha = ShieldSpriteRenderer.color.a;

        LeanTween.value(gameObject, startAlpha, targetAlpha, duration)
                .setOnUpdate((float alpha) => {
                     ShieldSpriteRenderer.color = new Color(ShieldSpriteRenderer.color.r, ShieldSpriteRenderer.color.g, ShieldSpriteRenderer.color.b, alpha);
                })
                .setOnComplete(onComplete);
    }

    public void DecreaseShieldHp(){
        ShieldCurrentHp--;
        if (ShieldCurrentHp == 0){
            StopBuff();
            return;
        }
        currentAlpha = currentAlpha - 1f / ShieldHp;
        FadeTo(currentAlpha, fadeDuration);
    }
}
