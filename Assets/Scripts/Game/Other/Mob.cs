using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mob : Creature
{
    [Header("Mob Settings")]
    [SerializeField] private int _scorePrice = 12;
    [SerializeField] private GameObject _mobSprite;
    [Range(0f, 1f)] public float SpawnChance = 1f;
    [Min(0f)][SerializeField] private float _initialSpeed = 1f;
    [HideInInspector] public float CurrentSpeed = 0f;
    private bool _isAttackingMelee = false;
    private Coroutine _meleeAttackCoroutine = null;

    [Header("Health Bar Settings")]
    [SerializeField] private GameObject _hpBar;
    [SerializeField] private float _hpBarYPos = .55f;

    private float _intervBetwMelee = 1f;

    void Start()
    {
        UpdHpBarPosAndRot();
        SetUpSpeed();
    }

    void Update()
    {
        MoveForward();
        UpdateSpriteLookDirection();
        UpdHpBarPosAndRot();
    }

    void MoveForward()
    {
        if(!_isAttackingMelee) transform.Translate(Vector3.up * CurrentSpeed * Time.deltaTime, Space.Self);
        if(transform.position.x > 20 || transform.position.y > 20 || transform.position.x < -20 || transform.position.y < -20) Destroy(gameObject);
    }

    void SetUpSpeed()
    {
        if (CurrentSpeed == 0f) CurrentSpeed = _initialSpeed;
        float zRotation = transform.eulerAngles.z;
        if (zRotation == 90f || zRotation == 270f)
        {
            CurrentSpeed /= 3f;
        }
    }

    void UpdateSpriteLookDirection() 
    {
        if (transform) 
        {
            _mobSprite.transform.rotation = Quaternion.identity;
            float xPosition = transform.position.x;
            float xScaleMultiplier = (xPosition < 0) ? 1 : -1;
            float updatedXScale = xScaleMultiplier * Mathf.Abs(_mobSprite.transform.localScale.x);
            _mobSprite.transform.localScale = new Vector3(updatedXScale, _mobSprite.transform.localScale.y, 1); 
        }
    }

    public void UpdHpBarPosAndRot()
    {
        _hpBar.transform.SetPositionAndRotation(
            new Vector3(transform.position.x, transform.position.y + _hpBarYPos, transform.position.z),
            Quaternion.identity
        );
    }

    // public override void OnToggleChange() {
    //     healthText.text = showNumbers ? $"{currentHealth}" : "";
    // }
    
    public void ActionOnCollisionWithNinja()
    {
        _isAttackingMelee = true;
        MeleeAttack(NinjaController.Instance.gameObject.GetComponent<Creature>());
    }

    public void ResetActionOnCollisionWithNinja()
    {
        _isAttackingMelee = false;
        if(_meleeAttackCoroutine != null) StopCoroutine(_meleeAttackCoroutine);
    }

    public override void ActionOnDestroy()
    {
        // add death animation
        GameManager.Instance.AddToScore(_scorePrice);
        Destroy(gameObject);
    }

    void MeleeAttack(Creature creature)
    {
        _meleeAttackCoroutine = StartCoroutine(MeleeAttackCoroutine(creature));
    }

    IEnumerator MeleeAttackCoroutine(Creature creature)
    {
        while(true)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collisionSound);
            float damage = AtckScr.CountTotalDamage();
            creature.TakeDamage(damage, AttackType.None);
            AtckScr.ApplyAttackEffects(creature);
            HpScr.Heal(AtckScr.LifeStealPerHit);
            yield return new WaitForSeconds(_intervBetwMelee);
        }
    }

    public void ResetSpeed()
    {
        CurrentSpeed = _initialSpeed;
    }
}

