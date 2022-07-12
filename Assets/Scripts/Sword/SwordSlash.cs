using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class SwordSlash : EnergyConsumingAction
{
    [SerializeField, Range(0.0001f, 1f)] private float _attackDuration = 0.1f;
    [SerializeField, Range(0f, 10000f)] private float _attackSpeed = 300f;
    public Transform _transform;
    public Animator _animator;
    public float _durationBoi = 0f;
    public float _currentAttackDirection = 0f;
    public int _currentFacingDirection;
    public Quaternion _defaultRotation;
    private float _translationFactorX = 0.05f;
    private float _translationFactorY = 0.6f;
    public bool IsAttacking = false;
    SoundManager _soundManager;


    private void Awake()
    {
        _actionName = "Slash";
        _action = () => PerformSlash();
        _soundManager = gameObject.GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();

        if (_agentState.IsSwinging)
        {
            var speed = -_attackSpeed / _attackDuration * _currentAttackDirection;
            Vector3 rotation = new Vector3(0, 0, speed) * Time.deltaTime;
            _transform.Rotate(rotation);
            _durationBoi += Time.deltaTime;
        }
        else IsAttacking = false;
    }

    private void PerformSlash()
    {
        if (_agentState.IsAttacking) return;
        IsAttacking = true;
        Debug.Log(_transform.rotation);

        _agentState.IsAttacking = true;
        _agentState.IsSwinging = true;
        _currentFacingDirection = _agentState.FacingDirection;

        _transform.Translate(_translationFactorX, _translationFactorY, 0f);

        _durationBoi = 0f;
        _currentAttackDirection = 0f;
        Invoke("StartSlash", 0.2f * _attackDuration);
        _soundManager.PlaySound("sword-swing");
    }

    private void StartSlash()
    {
        _currentAttackDirection = 1f;
        Invoke("PauseSlash", 0.4f * _attackDuration);

    }

    private void PauseSlash()
    {
        _currentAttackDirection = 0f;
        Invoke("HalfSlash", 0.2f * _attackDuration);
    }

    private void HalfSlash()
    {
        _currentAttackDirection = -2f;
        Invoke("EndSlash", 0.2f * _attackDuration);
    }

    private void EndSlash()
    {
        _agentState.IsAttacking = false;
        _agentState.IsSwinging = false;
        _transform.eulerAngles = new Vector3(0, 0, _agentState.FacingDirection * -33.538f);
        _transform.Translate(-_translationFactorX, -_translationFactorY, 0f);
    }
}
