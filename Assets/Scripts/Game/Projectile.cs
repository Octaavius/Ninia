using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Sprite To Rotate")]
    public GameObject spriteToRotate;
    [Header("Projectile basic settings")]
    [SerializeField] private float Speed = 2f;
    public Vector3 rotationSpeed = new Vector3(0, 0, 100);
    void Awake(){
        float zRotation = transform.eulerAngles.z;
        if (zRotation == 90f || zRotation == 270f)
        {
            Speed /= 3f;
        }
    }
    void Update()
    {
        MoveForward();
        Rotate();
    }

    public abstract void ActionOnDestroy(); 
    public abstract void ActionOnCollision(ref Health healthScript);

    private void MoveForward(){
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }
    
    public void Rotate(){
        spriteToRotate.transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public void setProjectileSpeed(float newSpeed) {
        Speed = newSpeed; 
        Debug.Log("Speed set to: " + Speed);
    }

}
