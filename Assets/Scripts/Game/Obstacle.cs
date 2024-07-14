using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    public int ScorePrice = 7;
    public int CoinsPrice = 0;
    void Awake(){
        float zRotation = transform.eulerAngles.z;
        if (zRotation == 90f || zRotation == 270f)
        {
            Speed /= 3f;
        }
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }

    public void setObstacleSpeed(float newSpeed) {
        Speed = newSpeed;   
    }
}