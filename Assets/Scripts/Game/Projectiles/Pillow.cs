using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Projectile
{
    public override void ActionOnCollision(){
	Destroy(gameObject);
    }

    public override void ActionOnDestroy(){
        Destroy(gameObject);
    }
}
