using UnityEngine;
using System.Collections;

public class Coin : Projectile
{
    [SerializeField] private int price = 7;
    public MagnetAnimation magnetAnimation;
    private float SpawnChance;
    public override float GetSpawnChance() => SpawnChance;

    void Start()
    {
        if(GameManager.Instance.magnetBuffActivated){
            StartCoroutine(CollectCoin());
        } else {
            Magnet.OnMagnetBuffActivatedChanged += OnMagnetBuffActivatedChanged;
        }
    }

    void OnDestroy()
    {
        Magnet.OnMagnetBuffActivatedChanged -= OnMagnetBuffActivatedChanged;
    }

    public override void ActionOnCollision(){
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.coinSound);
        GameManager.Instance.AddToCoins(price);
        Destroy(gameObject);
    }
    private void OnMagnetBuffActivatedChanged()
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
