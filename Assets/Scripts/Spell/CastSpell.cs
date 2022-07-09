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
        GameObject shotFireball = Instantiate(_fireball, this.transform.position, Quaternion.identity);
        shotFireball.GetComponent<FireballBehavior>()._direction = _agentState.FacingDirection;
    }
}

