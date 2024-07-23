using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    public Vector3 rotationSpeed = new Vector3(0, 0, 100);
    public int ScorePrice = 7;
    public int CoinsPrice = 0;
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

    private void MoveForward(){
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self)
    }
    
    public void Rotate(){
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public void setProjectileSpeed(float newSpeed) {
        Speed = newSpeed;   
    }
}