using UnityEngine;

public class Coin : Projectile
{
    [SerializeField] private int price = 7;  

    public override void ActionOnCollision(ref AudioManager am, ref Health healthScript, ref GameManager gameManager){
	    Destroy(gameObject);
    }

    public override void ActionOnDestroy(ref AudioManager am, ref GameManager gameManager){
        //am.PlaySFX
        gameManager.AddToCoins(price);
        Destroy(gameObject);
    }    
}
