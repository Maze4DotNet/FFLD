using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class BecomeInvisibleDuringCast : MonoBehaviour
{
    public AgentState _agentState;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _spriteRenderer.enabled = !_agentState.IsCasting;
    }
}

