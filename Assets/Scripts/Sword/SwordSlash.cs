using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class SwordSlash : EnergyConsumingAction
{
    [SerializeField, Range(0f, 1f)] private float _attackDuration = 0.1f;
    public Transform _transform;
    public Animator _animator;
    public float _durationBoi = 0f;
    public float _currentAttackDirection = 0f;
    public int _currentFacingDirection;
    public Quaternion _defaultRotation;

    private void Awake()
    {
        _actionName = "Slash";
        _action = () => PerformSlash();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();

        if (_agentState.IsAttacking)
        {
            var speed = -1000f * _currentAttackDirection;
            Vector3 rotation = new Vector3(0, 0, speed) * Time.deltaTime;
            _transform.Rotate(rotation);
            _durationBoi += Time.deltaTime;
        }
    }

    private void PerformSlash()
    {
        Debug.Log(_transform.rotation);

        _agentState.IsAttacking = true;
        _currentFacingDirection = _agentState.FacingDirection;
        _transform.Translate(0.13f, 0.073f, 0f);

        _durationBoi = 0f;
        _currentAttackDirection = 1f;
        Invoke("HalfSlash", _attackDuration);
    }

    private void HalfSlash()
    {
        _currentAttackDirection = -2f;
        Invoke("EndSlash", 0.5f * _attackDuration);
    }

    private void EndSlash()
    {
        _agentState.IsAttacking = false;
        Debug.Log($"Duration: {_durationBoi}");
        _transform.eulerAngles = new Vector3(0, 0, _agentState.FacingDirection * -33.538f);
        _transform.Translate(-0.13f, -0.073f, 0f);
    }
}
