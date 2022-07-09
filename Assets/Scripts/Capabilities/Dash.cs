using System;
using UnityEngine;

namespace FFLD
{

    [RequireComponent(typeof(Controller))]
    public class Dash : EnergyConsumingAction
    {
        public bool IsDashing { get; private set; }

        private Rigidbody2D _body;
        private Vector2 _velocity;

        private float _dashPower = 20f;
        private float _dashLength = 1f;

        // Start is called before the first frame update
        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _actionName = "Dash";
            _action = () => DashAction();
        }

        private void DashAction()
        {
            _agentState.IsDashing = true;
            _velocity = _body.velocity;
            _body.gravityScale = 0;
            _velocity.x += _dashPower * _agentState.FacingDirection;
            Invoke("DashEnds",_dashLength);
            _agentState.IsDashing = true;
            _body.velocity = _velocity;
        }

        private void DashEnds()
        {
            _agentState.IsDashing = false;
            _body.gravityScale = 1;
            _agentState.IsDashing = false;
        }
    }
}