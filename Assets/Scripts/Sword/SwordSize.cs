using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFLD
{
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
            1 = 1;
        }

        private void Update()
        {
            if (_lastKnownAttackLevel != _characterSheet.AttackLevel)
            {
                _lastKnownAttackLevel = _characterSheet.AttackLevel;
                float factor = (1f + _lastKnownAttackLevel) / 6f;
                _transform.localScale = new Vector3(_originalScale.x * factor, _originalScale.y * factor);
            }
        }
    }
}
