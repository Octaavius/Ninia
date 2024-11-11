using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    //[Header("Sprite To Rotate")]
    private GameObject spriteToRotate;
    [Header("Projectile basic settings")]
    
    [SerializeField] private float BasicSpeed;
    
    private float Speed;

    public float spawnChance;
    public Vector3 rotationSpeed = new Vector3(0, 0, 100);
    protected bool showNumbers = false;

    void Awake(){
        Speed = BasicSpeed;
        float zRotation = transform.eulerAngles.z;
        if (zRotation == 90f || zRotation == 270f)
        {
            Speed /= 3f;
        }

        spriteToRotate = transform.GetChild(0).gameObject;
    }
    protected void Update()
    {
        MoveForward();
        Rotate();
        UpdateHealthBar();
    }

    public virtual void UpdateHealthBar(){}

    public virtual bool TakeDamage(float damage){ // by default just kill projectile, as it can be without hp
        ActionOnDestroy();
        return true;
    }

    public abstract void ActionOnDestroy();
    public abstract void ActionOnCollision();
    public virtual float GetSpawnChance() => 0.0f;
    public virtual void OnToggleChange() { }    
    private void MoveForward(){
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
        if(transform.position.x > 20 || transform.position.y > 20 || transform.position.x < -20 || transform.position.y < -20) Destroy(gameObject);
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
    public void ResetSpeed(){
        Speed = BasicSpeed;
    }
    public void SetSpawnChance(float newSpawnChance){
        spawnChance = newSpawnChance;
    }
    public void SetShowNumbers(bool show)
    {
        showNumbers = show;
        OnToggleChange();
    }
    protected void ActivateBuff<T>() where T : Buff
    {
        T Buff = BuffsManager.Instance.GetComponentInChildren<T>();
        Buff.StartBuff();
    }

}
