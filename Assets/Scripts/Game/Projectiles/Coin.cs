using UnityEngine;

public class Coin : Projectile
{
    [SerializeField] private int price = 7;  

    public override void ActionOnCollision(ref Health healthScript){
        Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        //am.PlaySFX
        GameManager.Instance.AddToCoins(price);
        Destroy(gameObject);
    }    
}
