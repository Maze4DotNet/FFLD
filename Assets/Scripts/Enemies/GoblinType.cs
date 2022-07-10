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
    [SerializeField, Range(0f,100f)] public float _knockBackY;

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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherObject = collision.gameObject;
        if (otherObject.name.Contains("Sword"))
        {
            var slash = otherObject.GetComponent<SwordSlash>();
            int direction = 1;
            if (otherObject.transform.position.x > gameObject.transform.position.x) direction = -1;
            
            if (slash.IsAttacking) TakeDamage(direction);
        }
    }

    private void TakeDamage(int direction)
    {
        if (_agentState.IsInvincible) return;
        _hp--;
        _body.velocity = new Vector2(direction*_knockBackX,_knockBackY);
        _agentState.IsTakingDamage = true;
        _agentState.IsInvincible = true;
        string nextAction = "Die";
        if (_hp > 0) nextAction = "Recover";
        Invoke(nextAction, _invincibilityPeriod);
    }

    private void Die()
    {
        
        Destroy(gameObject);
    }

    private void Recover()
    {
        _agentState.IsTakingDamage = false;
        _agentState.IsInvincible = false;

    }
}
