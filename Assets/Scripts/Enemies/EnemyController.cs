using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _direction;
    public float _roamDirection;
    private int _state = 0; //0 = roaming, 1 = chasing
    private GameObject _player;
    public Transform _transform;
    public AgentState _agentState;
    public GoblinType _type;
    [SerializeField, Range(0f, 100f)] private float _reloadTime;
    private bool _reloading = false;

    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Roaming());
    }

    // Update is called once per frame
    void Update()
    {
        if (_agentState.IsTakingDamage) return;
        var xDifference = _player.transform.position.x - _transform.position.x;
        var yDifference = _player.transform.position.y - _transform.position.y;

        float attackableDistanceX;

        if (_type._weaponType == 0) attackableDistanceX = 100;
        else attackableDistanceX = 1;

        if (_agentState.IsAttacking)
        {
            _direction = 0;
            return;
        }

        if (!_reloading && xDifference > -1 * attackableDistanceX && xDifference < 1 * attackableDistanceX && yDifference > -1 && yDifference < 1 && _state == 1)
        {
            //doe hier aanval dinge
            _agentState.IsAttacking = true;
            _type.Attack();
            Invoke("ReloadWeapon", _reloadTime);
        }
        else
        {
            _agentState.IsAttacking = false;
            if (_player.transform.position.x > _transform.position.x && _direction == 1 ||
                _player.transform.position.x < _transform.position.x && _direction == -1)
            {
                _state = 1;
            }
            if (_state == 0)
            {
                _direction = _roamDirection;
            }
            else if (_state == 1)
            {
                if (_player.transform.position.x > _transform.position.x)
                {
                    _direction = 1;
                }
                else
                {
                    _direction = -1;
                }
            }
        }
    }

    public void ReloadWeapon()
    {
        Invoke("EndReloadWeapon", _reloadTime);
        _agentState.IsAttacking = false;
        _reloading = true;
    }

    public void EndReloadWeapon()
    {
        _reloading = false;
    }

    IEnumerator Roaming()
    {
        _roamDirection = randomDirection();
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        StartCoroutine(Roaming());
    }
    private float randomDirection()
    {
        float val = Random.Range(-1f, 1f);
        if (val > 0)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }

    private void OnDestroy()
    {
        _player.GetComponent<CharacterSheet>().GainInexperience();
    }
}
