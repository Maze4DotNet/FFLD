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
    SoundManager _soundManager;

    private void Awake()
    {
        _action = () => PerformSpell();
        _actionName = "Spell";
        _soundManager = gameObject.GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();

        // whatever er verder moet gebeuren
    }

    private void PerformSpell()
    {
        // cast de spellllll yo
        if (_agentState.IsAttacking == true || _characterSheet.Mana != 100)
        {
            _successful = false;
            return;
        }

        _characterSheet.Mana = 0;

        _agentState.IsAttacking = true;
        _agentState.IsCasting = true;
        _soundManager.PlaySound("magic");
        var pos = this.transform.position + new Vector3(_agentState.FacingDirection*0.5f,0);
        GameObject shotFireball = Instantiate(_fireball, pos, Quaternion.identity);
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

