using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class CastSpell : EnergyConsumingAction
{
    Rigidbody2D _body;
    public GameObject _fireball;
    [SerializeField, Range(0f, 1f)] private float _attackDuration = 0.1f;

    private void Awake()
    {
        _action = () => PerformSpell();
        _actionName = "Spell";
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();

        // whatever er verder moet gebeuren
    }

    private void PerformSpell()
    {
        // cast de spellllll yo
        if (_agentState.IsAttacking == true) return;

        _agentState.IsAttacking = true;
        _agentState.IsCasting = true;
        GameObject shotFireball = Instantiate(_fireball, this.transform.position, Quaternion.identity);
        FireballBehavior behavior = shotFireball.GetComponent<FireballBehavior>();
        behavior._direction = _agentState.FacingDirection;
        behavior._characterSheet = _characterSheet;
        behavior.DieEneNaAwake();
        Invoke("EndSpellAnimation", _attackDuration);
    }

    private void EndSpellAnimation()
    {
        _agentState.IsAttacking = false;
        _agentState.IsCasting = false;
    }
}

