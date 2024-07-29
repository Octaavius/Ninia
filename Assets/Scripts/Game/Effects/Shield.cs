using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Effect
{
    public GameObject ShieldObject;
 
    private SpriteRenderer ShieldSpriteRenderer;
    public float fadeDuration = 1f;

    void Start(){
        ShieldSpriteRenderer = ShieldObject.GetComponent<SpriteRenderer>();
    }

    protected override void ActivateEffect()
    {
        ShieldObject.SetActive(true);
        FadeTo(1f, fadeDuration);
    }

    protected override void DisactivateEffect()
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
}
