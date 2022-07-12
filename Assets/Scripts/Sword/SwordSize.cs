using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordSize : MonoBehaviour
{
    private int _lastKnownAttackLevel;
    private Transform _transform;
    private Vector2 _originalScale;
    public CharacterSheet _characterSheet;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _originalScale = transform.localScale;
        _lastKnownAttackLevel = _characterSheet.AttackLevel;
        SetSize();
    }

    private void Update()
    {
        if (_lastKnownAttackLevel != _characterSheet.AttackLevel)
        {
            SetSize();
        }
        if (_characterSheet.Hp <= 0) Destroy(gameObject);
    }

    private void SetSize()
    {
        _lastKnownAttackLevel = _characterSheet.AttackLevel;
        float factor = (2f + _lastKnownAttackLevel) / 6f;
        _transform.localScale = new Vector3(_originalScale.x * factor, _originalScale.y * factor, 1);

    }
}
