using UnityEngine;
using System;

public class EnemyMove : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private Ground _ground;
    public Transform _transform;
    public EnemyController _enemyController;
    public AgentState _agentState;

    private float _maxSpeedChange, _acceleration;
    private bool _onGround;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _direction.x = _enemyController._direction;
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);

        if (_direction.x != 0)
        {
            _agentState.IsWalking = true;
            _agentState.FacingDirection = Math.Sign(_direction.x);
            var scale = _transform.localScale;
            _transform.localScale = new Vector2(Math.Sign(_direction.x) * Math.Abs(_transform.localScale.x), scale.y);
        }
        else _agentState.IsWalking = false;
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
