using UnityEngine;
using System;

namespace FFLD
{

    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

        private Controller _controller;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody2D _body;
        private Ground _ground;
        public AgentState _agentState;
        public CharacterSheet _characterSheet;

        private float _maxSpeedChange, _acceleration;
        private bool _onGround;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
            _controller = GetComponent<Controller>();
        }

        private void Update()
        {
            _direction.x = _controller.input.RetrieveMoveInput();
            if (_characterSheet.Energy == 0) _direction.x = 0f;

            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);

            if (_direction.x != 0) _agentState.FacingDirection = Math.Sign(_direction.x);
        }

        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _velocity = _body.velocity;

            _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
            _maxSpeedChange = _acceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

            _body.velocity = _velocity;
        }
    }
}