using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mob : Creature
{
    [Header("Mob Settings")]
    [SerializeField] private int ScorePrice = 12;
    [SerializeField] private GameObject MobSprite;
    [Range(0f, 1f)]
    public float SpawnChance = 1f;
    [Min(0f)]
    public float Speed = 1f;
    [HideInInspector] public float CurrentSpeed = 0f;

    [Header("Health Bar Settings")]
    [SerializeField] private GameObject HpBar;
    [SerializeField] private float HpBarYPos = .55f;

    private float IntervBetwMelee = 1f;

    void Start()
    {
        SetSpriteScale();
        UpdHpBarPosAndRot();
        SetUpSpeed();
    }

    void Update()
    {
        MoveForward();
        UpdHpBarPosAndRot();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
        if(transform.position.x > 20 || transform.position.y > 20 || transform.position.x < -20 || transform.position.y < -20) Destroy(gameObject);
    }

    void SetUpSpeed()
    {
        if (CurrentSpeed == 0f) CurrentSpeed = Speed;
        float zRotation = transform.eulerAngles.z;
        if (zRotation == 90f || zRotation == 270f)
        {
            CurrentSpeed /= 3f;
        }
    }

    void SetSpriteScale() 
    {
        if (transform) 
        {
            MobSprite.transform.rotation = Quaternion.identity;
            float rotationAngle = transform.eulerAngles.z;
            if (rotationAngle == 0 || rotationAngle == 90) 
            {
                MobSprite.transform.localScale = new Vector3(-MobSprite.transform.localScale.x, MobSprite.transform.localScale.y, 1); 
            }
        }
    }

    public void UpdHpBarPosAndRot()
    {
        HpBar.transform.SetPositionAndRotation(
            new Vector3(transform.position.x, transform.position.y + HpBarYPos, transform.position.z),
            Quaternion.identity
        );
    }

    // public override void OnToggleChange() {
    //     healthText.text = showNumbers ? $"{currentHealth}" : "";
    // }
    
    public void ActionOnCollisionWithNinja()
    {
        Speed = 0;
        MeleeAttack(NinjaController.Instance.gameObject.GetComponent<Creature>());
    }

    public override void ActionOnDestroy()
    {
        // add death animation
        GameManager.Instance.AddToScore(ScorePrice);
        Destroy(gameObject);
    }

    void MeleeAttack(Creature creature)
    {
        StartCoroutine(MeleeAttackCoroutine(creature));
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
            yield return new WaitForSeconds(IntervBetwMelee);
        }
    }

    public void ResetSpeed()
    {
        CurrentSpeed = Speed;
    }
}

