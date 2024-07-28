using UnityEngine;
using System.Collections;

public class Coin : Projectile
{
    [SerializeField] private int price = 7;  
    public MagnetAnimation magnetAnimation;

    void Start()
    {
        if(GameManager.Instance.magnetEffectActivated){
            StartCoroutine(CollectCoin());
        } else {
            Magnet.OnMagnetEffectActivatedChanged += OnMagnetEffectActivatedChanged;
        }
    }

    void OnDestroy()
    {
        Magnet.OnMagnetEffectActivatedChanged -= OnMagnetEffectActivatedChanged;
    }

    public override void ActionOnCollision(ref Health healthScript){
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.coinSound);
        GameManager.Instance.AddToCoins(price);
        Destroy(gameObject);
    }

    private void OnMagnetEffectActivatedChanged()
    {
        StartCoroutine(CollectCoin());
    }

    IEnumerator CollectCoin()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        magnetAnimation.doAnimation();
        yield return new WaitForSeconds(magnetAnimation.animationDuration);
        ActionOnDestroy();
    }
}
