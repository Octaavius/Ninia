using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Sprite To Rotate")]
    public GameObject spriteToRotate;
    [Header("Projectile basic settings")]
    [SerializeField] public float Speed = 2.0f;
    public float spawnChance;
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
    public abstract void ActionOnCollision();
    public virtual float GetSpawnChance() => 0.0f;

    private void MoveForward(){
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }
    
    public void Rotate(){
        spriteToRotate.transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public void SetProjectileSpeed(float newSpeed){
        Speed = newSpeed;
    }
    public float GetCurrentSpeed(){
        return Speed;
    }
    public void SetSpawnChance(float newSpawnChance){
        spawnChance = newSpawnChance;
    }
    protected void ActivateEffect<T>() where T : Effect
    {
        T effect = EffectsManager.Instance.GetComponentInChildren<T>();
        effect.StartEffect();
    }
}
