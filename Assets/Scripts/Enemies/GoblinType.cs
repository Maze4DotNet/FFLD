using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinType : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] public float _size;
    [SerializeField, Range(0, 100)] public int _hp;
    [SerializeField, Range(0, 6)] public int _damage;
    [SerializeField, Range(0f, 100f)] public float _invincibilityPeriod;
    [SerializeField, Range(0f, 100f)] public float _knockBackX;
    [SerializeField, Range(0f, 100f)] public float _knockBackY; 
    [SerializeField, Range(0f, 1f)] public float _sizeFactor = 0.6f;
    [SerializeField] public bool _hasCrown;
    SoundManager _soundManager;


    public GameObject _victoryScreen;

    private GoblinSpawnScript _spawnScript;
    /// <summary>
    /// 0 for dagger, 1 for sword.
    /// </summary>
    [SerializeField, Range(0, 6)] public int _weaponType;

    public AgentState _agentState;
    public Rigidbody2D _body;
    public GameObject _weapon;
    public WearCrown _wearCrown;

    public void Awake()
    {
        _soundManager = gameObject.GetComponent<SoundManager>();
    }

    public void Attack()
    {
        var pos = this.transform.position + new Vector3(_agentState.FacingDirection * 0.5f, 0);
        GameObject newWeapon = Instantiate(_weapon, pos, Quaternion.identity);
        var scale = newWeapon.transform.localScale;
        var sizey = 1 + (_size - 1) * 2;
        newWeapon.transform.localScale = new Vector3(scale.x * sizey, scale.y * sizey, scale.z * sizey);
        GoblinWeaponScript weaponScript = newWeapon.GetComponent<GoblinWeaponScript>();
        AgentState agentState = GetComponent<AgentState>();
        weaponScript.Initialize(gameObject, this, _weaponType, agentState.FacingDirection, _damage);
        weaponScript._toughness = Math.Min((int)(3f/_sizeFactor),(int)_size);
        _soundManager.PlaySound("knife-throw");
        weaponScript.Attack();
    }

    internal void WhosYourDaddy(GoblinSpawnScript goblinSpawnScript, int toughness)
    {
        _size = toughness == 1 ? toughness : toughness * _sizeFactor;
        _damage = toughness;
        _hp = 2 * toughness - 1;
        _spawnScript = goblinSpawnScript;
        var scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector2(scale.x * _size, scale.y * _size);
        var render = gameObject.GetComponent<SpriteRenderer>();
        float die = 0.1f;
        float dat = die * (toughness - 1);
        if (toughness < 4) _wearCrown.NoCrown();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var otherObject = collision.gameObject;
        if (otherObject.name.Contains("Sword"))
        {
            var slash = otherObject.GetComponent<SwordSlash>();
            int direction = 1;
            if (otherObject.transform.position.x > gameObject.transform.position.x) direction = -1;

            if (slash.IsAttacking) TakeDamage(direction, 1);
        }
        else if (otherObject.name.Contains("Explosion") || otherObject.name.Contains("Fireball"))
        {
            int direction = 1;
            if (otherObject.transform.position.x > gameObject.transform.position.x) direction = -1;
            TakeDamage(direction, 2);
            if (otherObject.name.Contains("Fireball")) Destroy(otherObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherObject = collision.gameObject;
        if (otherObject.name.Contains("Character") && !_agentState.IsTakingDamage)
        {
            var sheet = otherObject.GetComponent<CharacterSheet>();
            sheet.TakeDamage(gameObject, _damage);
        }
    }

    private void TakeDamage(int direction, int damage)
    {
        if (_agentState.IsInvincible) return;
        _hp -= damage;
        _body.velocity = new Vector2(direction * _knockBackX, _knockBackY);
        _agentState.IsTakingDamage = true;
        _agentState.IsInvincible = true;
        string nextAction = "Die";
        if (_hp > 0) nextAction = "Recover";
        Invoke(nextAction, _invincibilityPeriod);
        _soundManager.PlaySound("hurt-enemy");
    }

    private void Die()
    {
        _spawnScript.Died(transform.position, _damage);
        if (_damage == 4)
        {
            Instantiate(_victoryScreen);
        }
        Destroy(gameObject);
    }

    private void Recover()
    {
        _agentState.IsTakingDamage = false;
        _agentState.IsInvincible = false;

    }

    private void FixedUpdate()
    {
         if (_spawnScript._goblorIsHere && _damage < 4) Destroy(gameObject);
    }
}
