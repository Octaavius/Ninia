using UnityEngine;

public abstract class Projectile : MonoBehaviour, IHitable
{
    //[Header("Sprite To Rotate")]
    private GameObject _spriteToRotate;
    [Header("Projectile basic settings")]
    
    [SerializeField] private float BasicSpeed;
    
    private float _speed;

    public Vector3 rotationSpeed = new Vector3(0, 0, 100);

    void Awake(){
        _speed = BasicSpeed;
        float zRotation = transform.eulerAngles.z;
        if (zRotation == 90f || zRotation == 270f)
        {
            _speed /= 3f;
        }

        _spriteToRotate = transform.GetChild(0).gameObject;
    }
    protected void Update()
    {
        MoveForward();
        Rotate();
    }

    public bool TakeDamage(float damage, AttackType attackType){ // by default just kill projectile, as it can be without hp
        ActionOnDestroy();
        return true;
    }

    public abstract void ActionOnDestroy();
    public abstract void ActionOnCollision();

    private void MoveForward(){
        transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
        if(transform.position.x > 20 || transform.position.y > 20 || transform.position.x < -20 || transform.position.y < -20) Destroy(gameObject);
    }
    
    public void Rotate(){
        _spriteToRotate.transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public void SetProjectileSpeed(float newSpeed){
        _speed = newSpeed;
    }
    public float GetCurrentSpeed(){
        return _speed;
    }
    public void ResetSpeed(){
        _speed = BasicSpeed;
    }
    protected void ActivateBuff<T>() where T : Buff
    {
        T Buff = BuffsManager.Instance.GetComponentInChildren<T>();
        Buff.StartBuff();
    }

}
