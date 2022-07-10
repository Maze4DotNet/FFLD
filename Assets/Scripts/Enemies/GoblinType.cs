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
    private GoblinSpawnScript _spawnScript;
    /// <summary>
    /// 0 for dagger, 1 for sword.
    /// </summary>
    [SerializeField, Range(0, 6)] public int _weaponType;

    public AgentState _agentState;
    public Rigidbody2D _body;
    public GameObject _weapon;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        var pos = this.transform.position + new Vector3(_agentState.FacingDirection * 0.5f, 0);
        GameObject newWeapon = Instantiate(_weapon, pos, Quaternion.identity);

        GoblinWeaponScript weaponScript = newWeapon.GetComponent<GoblinWeaponScript>();
        AgentState agentState = GetComponent<AgentState>();
        weaponScript.Initialize(gameObject, this, _weaponType, agentState.FacingDirection, _damage);
        weaponScript.Attack();
    }

    internal void WhosYourDaddy(GoblinSpawnScript goblinSpawnScript, int toughness)
    {
        _size = toughness;
        _damage = toughness;
        _hp = 2*toughness - 1;
        _spawnScript = goblinSpawnScript;
        var scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector2(scale.x * toughness, scale.y * toughness);
        var render = gameObject.GetComponent<SpriteRenderer>();
        float die = 0.1f;
        float dat = die * (toughness - 1);
        render.material.color += new Color(dat, dat, dat);

    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    }

    private void Die()
    {
        _spawnScript.Died();
        Destroy(gameObject);
    }

    private void Recover()
    {
        _agentState.IsTakingDamage = false;
        _agentState.IsInvincible = false;

    }
}
