using System;
using UnityEngine;

namespace FFLD
{

    [RequireComponent(typeof(Controller))]
    public class Dash : EnergyConsumingAction
    {
        [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
        [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

        private Controller _controller;
        private Rigidbody2D _body;
        private Ground _ground;
        private Vector2 _velocity;

        private int _dashPhase;
        private float _defaultGravityScale, _jumpSpeed;

        private bool _desiredDash, _onGround;

        private float _dashPower = 20f;
        private float _dashLength = 1f;
        private float _dashCooldown = 1f;


        // Start is called before the first frame update
        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
            _controller = GetComponent<Controller>();
            _actionName = "Dash";
            _action = () => DashAction();

            _defaultGravityScale = 1f;
        }

        private void DashAction()
        {
            _velocity = _body.velocity;
            Debug.Log("Ja, hij doet de dash action");
            _body.gravityScale = 0;
            _velocity.x += _dashPower * _agentState.FacingDirection;
            Invoke("DashEnds",_dashLength);
            _agentState.IsDashing = true;
            _body.velocity = _velocity;
        }

        private void DashEnds()
        {
            Debug.Log("Dash Ends");
            _body.gravityScale = 1;
            _agentState.IsDashing = false;
        }
    }
}