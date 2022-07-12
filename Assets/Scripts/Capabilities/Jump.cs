using UnityEngine;


[RequireComponent(typeof(Controller))]
public class Jump : EnergyConsumingAction
{
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 5f)] private int _airJumps = 1;
    [SerializeField, Range(0f, 5f)] private int _jumpPhase = 0;



    public bool IsAirborne { get { return !_ground.OnGround; } }

    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity;

    private float _defaultGravityScale, _jumpSpeed;


    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _actionName = "Jump";
        _action = () => JumpAction();

        _defaultGravityScale = 1f;
    }

    private void FixedUpdate()
    {
        _velocity = _body.velocity;

        if (!_agentState.IsAirborne) _jumpPhase = 0;

        base.FixedUpdate();

        if (_agentState.IsDashing) _body.gravityScale = 0;
        else if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (_body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if (_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }

        _body.velocity = _velocity;
    }

    private void JumpAction()
    {
        if (!_agentState.IsAirborne) _successful = false;
        if (_jumpPhase >= _airJumps)
        {
            _successful = false;
            return;
        }

        _jumpPhase++;

        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

        if (_velocity.y > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
        }
        else if (_velocity.y < 0f)
        {
            _jumpSpeed += Mathf.Abs(_body.velocity.y);
        }
        _velocity.y += _jumpSpeed;
        _agentState.IsDashing = false;
    }
}