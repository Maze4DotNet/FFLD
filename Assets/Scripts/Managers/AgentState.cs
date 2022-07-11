﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AgentState : MonoBehaviour
{
    [SerializeField] private int _facingDirection = 1;
    [SerializeField] private bool _isPlayer = true;

    public bool IsDashing { get; set; }
    public bool IsAirborne { get { return !_ground.OnGround; } }
    public bool IsAttacking { get; set; }
    public bool IsCasting { get; set; }
    public bool IsSwinging { get; set; }
    public bool IsWalking { get; set; }
    public bool IsTakingDamage { get; set; }
    public bool IsDead { get; set; }
    public bool IsCelebrating { get; set; }
    public bool IsTired { get; set; }

    public bool CantMove
    {
        get
        {
            return
                IsTired ||
                IsAttacking ||
                IsDead ||
                IsTakingDamage;
        }
    }


    private Animator _animator;

    private Ground _ground;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _ground = GetComponent<Ground>();
    }

    public int FacingDirection
    {
        get
        {
            return _facingDirection;
        }
        set
        {
            _facingDirection = value;
        }
    }

    public bool IsInvincible { get; internal set; }

    private void Update()
    {
        _animator.SetBool("isWalking", IsWalking);
        _animator.SetBool("isAttacking", IsAttacking);
        _animator.SetBool("isAirborne", IsAirborne);
        _animator.SetBool("isDamaged", IsTakingDamage);

        if (!_isPlayer) return;
        _animator.SetBool("isDashing", IsDashing);
        _animator.SetBool("isDead", IsDead);
        _animator.SetBool("isCelebrating", IsCelebrating);
        _animator.SetBool("isTired", IsTired);
    }

    internal void Die()
    {
        IsDead = true;
        IsDashing = false;
        IsAttacking = false;
        IsCasting = false;
        IsSwinging = false;
        IsWalking = false;
        IsTakingDamage = false;
        IsCelebrating = false;
        IsTired = false;
    }
}