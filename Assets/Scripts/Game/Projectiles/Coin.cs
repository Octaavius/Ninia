using UnityEngine;

public class Coin : Projectile
{
    public override void ActionOnCollision(){
	Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        Destroy(gameObject);
    }    
}
