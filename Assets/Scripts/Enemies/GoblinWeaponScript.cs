using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GoblinWeaponScript:MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] public float _speedX = 100f;
    [SerializeField, Range(0f, 100f)] public float _speedY = 100f;
    [SerializeField, Range(0f, 1000f)] public float _rotationSpeed = 10f;

    public Rigidbody2D _body;
    private BoxCollider2D _collider;

    public int _toughness;

    private GameObject _goblinObject;
    private GoblinType _goblinType;
    private int _weaponType;
    private int _direction;
    public int _damage;
    [SerializeField, Range(0f, 50f)] private float _lifeTime = 2f;

    public GoblinType Type { get { return _goblinType; } set { _goblinType = value; } }

    public void Initialize(GameObject goblinObject, GoblinType goblinType, int weaponType, int direction, int damage)
    {
        _goblinObject = goblinObject;
        _goblinType = goblinType;
        _weaponType = weaponType;
        _direction = direction;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherObject = collision.gameObject;
        if (_goblinObject is null) return;
        if (otherObject != _goblinObject)
        {
            if (otherObject.name.Contains("Character"))
            {
                var sheet = otherObject.GetComponent<CharacterSheet>();
                sheet.TakeDamage(gameObject,_damage);
            }
            else if (otherObject.name.Contains("Trigger")) return;
            else if (otherObject.name.Contains("Sword"))
            {
                SwordSlash swordSlash = otherObject.GetComponent<SwordSlash>();
                if (!swordSlash.IsAttacking) return;
            }
            if (_toughness > 1) return;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        var rotationSpeed = -_rotationSpeed * _direction;
        Vector3 rotation = new Vector3(0, 0, rotationSpeed) * Time.deltaTime;
        gameObject.transform.Rotate(rotation);

    }

    public void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        StartCoroutine(WaitThenDie());
    }

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    public void Attack()
    {
        if (_weaponType == 0) Throw();
        else Swing();
    }

    public void Throw()
    {
        var newVelocity = new Vector2(_direction * _speedX*_toughness, 1f * _speedY* _toughness);
        _body.velocity = newVelocity;
    }

    public void Swing()
    {

    }
}
