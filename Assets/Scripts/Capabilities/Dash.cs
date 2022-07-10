using System;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Dash : EnergyConsumingAction
{
    private Rigidbody2D _body;
    private Vector2 _velocity;

    [SerializeField, Range(1f, 100f)] private float _dashPowerXGroundFactor = 1f;
    [SerializeField, Range(0f, 100f)] private float _dashPowerX = 20f;
    [SerializeField, Range(0f, 100f)] private float _dashPowerY = 0;
    [SerializeField, Range(0f, 1f)] private float _dashLength = 0.3f;




    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _actionName = "Dash";
        _action = () => DashAction();
    }

    private void DashAction()
    {
        if (_agentState.CantMove || _agentState.IsDashing) return;

        _agentState.IsDashing = true;
        _velocity = _body.velocity;
        _velocity.y = 0f;
        //_body.gravityScale = 0;

        var xDashPower = _dashPowerX * _agentState.FacingDirection;
        var yDashPower = _dashPowerY;

        if (!_agentState.IsAirborne) xDashPower *= _dashPowerXGroundFactor;

        _velocity.x += xDashPower;
        _velocity.y += yDashPower;
        Invoke("DashEnds", _dashLength);
        _body.velocity = _velocity;
    }

    private void DashEnds()
    {
        //_body.gravityScale = 1;
        _agentState.IsDashing = false;
    }
}
