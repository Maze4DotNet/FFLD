using System;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    #region FIELDS
    [SerializeField, Range(0, 5)] private int _attackLevel = 5;
    [SerializeField, Range(0, 5)] private int _defenseLevel = 5;
    [SerializeField, Range(0, 5)] private int _enduranceLevel = 5;
    [SerializeField, Range(0, 5)] private int _magicLevel = 5;

    [SerializeField, Range(0, 24)] private int _hp = 18;
    [SerializeField, Range(0, 24)] private int _energy = 18;


    [SerializeField, Range(0, 3)] private float _energyRechargeTime;
    [SerializeField, Range(0, 10)] private int _rechargeCounter = 0;
    [SerializeField, Range(0, 10)] private int _rechargeFactor = 1;
    [SerializeField, Range(0, 10)] private int _rechargeFactorTired = 3;

    [SerializeField, Range(0, 3)] private float _manaRechargeTime;
    [SerializeField, Range(0, 100)] private int _mana = 100;
    [SerializeField, Range(0, 10)] private int _manaRechargeFactor = 1;


    [SerializeField, Range(0f, 100f)] private float _invincibilityDuration;
    [SerializeField, Range(0f, 100f)] private float _knockBackX;


    [SerializeField, Range(0f, 100f)] private float _knockBackY;

    [SerializeField, Range(0, 10)] public int _inexperiencePoints = 0;
    [SerializeField, Range(0, 100)] public int _requiredPoints = 10;

    [SerializeField] public bool _invincibleMode = false;

    SoundManager _soundManager;

    private int _currentRechargeFactor = 1;

    public AgentState _agentState;
    private int _rechargeMax = 10;
    private float _energyTime = 0;
    private float _manaTime = 0;

    public Rigidbody2D _body;

    public GameObject _downLevelMenu;
    public GameObject _introScreen;

    #endregion FIELDS


    #region PROPERTIES
    public int MaxEnergy { get { return 4 * (EnduranceLevel + 1); } }

    public int MaxHP { get { return 4 * (DefenseLevel + 1); } }
    public int AttackLevel
    {
        get { return _attackLevel; }
        set { _attackLevel = value; }
    }

    public int DefenseLevel
    {
        get { return _defenseLevel; }
        set { _defenseLevel = value; }
    }

    public int EnduranceLevel
    {
        get
        {
            return _enduranceLevel;
        }
        set
        {
            _enduranceLevel = value;
        }
    }

    public int MagicLevel
    {
        get
        {
            return _magicLevel;
        }
        set
        {
            _magicLevel = value;
        }
    }

    public int Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
        }
    }
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }

    public int Mana
    {
        get
        {
            return _mana;
        }
        set
        {
            _mana = value;
        }
    }

    public int TotalLevel
    {
        get
        {
            return AttackLevel + DefenseLevel + EnduranceLevel + MagicLevel;
        }
    }
    #endregion PROPERTIES

    #region METHODS
    internal void RestoreEnergy()
    {
        _energy = MaxEnergy;
        _agentState.IsTired = false;
    }
    internal void Heal()
    {
        if (_hp < MaxHP && !_agentState.IsDead)
        {
            _hp += 4;
        }
    }
    internal void TakeDamage(GameObject otherObject, int damage)
    {
        if (_invincibleMode || _agentState.IsInvincible) return;
        _hp -= damage;
        if (_hp <= 0)
        {
            _agentState.Die();
            //doodgaan
            return;
        }

        int flyDir;
        var otherPos = otherObject.transform.position;
        var myPos = gameObject.transform.position;
        if (otherPos.x > myPos.x) flyDir = -1;
        else flyDir = 1;

        var newVelocity = new Vector2(flyDir * _knockBackX, _knockBackY);
        _body.velocity += newVelocity;

        _agentState.IsTakingDamage = true;
        _agentState.IsInvincible = true;
        Invoke("TakingDamageEnds", _invincibilityDuration);
        _soundManager.PlaySound("hurt-player");
    }

    internal void GainInexperience()
    {
        _inexperiencePoints++;
    }

    public void TakingDamageEnds()
    {
        _agentState.IsTakingDamage = false;
        Invoke("InvincibilityEnds", _invincibilityDuration);
    }

    public void InvincibilityEnds()
    {
        _agentState.IsInvincible = false;
    }

    private void Awake()
    {
        Instantiate(_introScreen);
        _soundManager = gameObject.GetComponent<SoundManager>();
    }
    private void Update()
    {
        _energyTime += Time.deltaTime;
        _manaTime += Time.deltaTime;

        if (_energyTime > _currentRechargeFactor * _energyRechargeTime)
        {
            _energyTime = 0;
            IncreaseEnergy();
        }

        if (_hp > MaxHP) _hp = MaxHP;

        if (Mana < 100 && _manaTime > _manaRechargeFactor * _manaRechargeTime)
        {
            _manaTime = 0;
            IncreaseMana();
        }

        if (_inexperiencePoints >= 4 && TotalLevel != 0)//_requiredPoints)
        {
            LevelDown();
        }
    }

    private void LevelDown()
    {
        _inexperiencePoints = 0;
        //_requiredPoints = Math.Min(10, _requiredPoints + 1);
        Instantiate(_downLevelMenu);
        //Time.timeScale = 0f;
        GetComponent<GoblinSpawnScript>();
    }

    public void DecreaseEnergy(string caller, bool successful)
    {
        if (!successful) return;

        if (Energy == 0)
        {
            Debug.Log($"WARNING:\n{caller} tried to decrease energy below 0.");
            return;
        }
        _rechargeCounter = 0;
        Energy--;
        if (Energy == 0)
        {
            _currentRechargeFactor = _rechargeFactorTired;
            _agentState.IsTired = true;
        }
    }

    public void IncreaseEnergy()
    {
        if (Energy == MaxEnergy) return;

        // Increase the recharge counter.
        _rechargeCounter = System.Math.Min(_rechargeMax, _rechargeCounter + 1);

        // If the recharge counter is at 10, gain an energy cell.
        if (_rechargeCounter < _rechargeMax) return;
        _agentState.IsTired = false;

        _currentRechargeFactor = _rechargeFactor;
        Energy = System.Math.Min(MaxEnergy, Energy + 1);
        _rechargeCounter = 0;
    }

    public void IncreaseMana()
    {
        Mana++;
    }
    #endregion METHODS
}
