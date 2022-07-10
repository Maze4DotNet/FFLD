using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    #region FIELDS
    [SerializeField, Range(0, 5)] private int _attackLevel = 5;
    [SerializeField, Range(0, 5)] private int _defenseLevel = 5;
    [SerializeField, Range(0, 5)] private int _enduranceLevel = 5;
    [SerializeField, Range(0, 5)] private int _magicLevel = 5;

    [SerializeField, Range(0, 6)] private int _hp = 6;
    [SerializeField, Range(0, 12)] private int _energy = 12;
    [SerializeField, Range(0, 3)] private float _energyRechargeTime;
    [SerializeField, Range(0, 10)] private int _rechargeCounter = 0;
    [SerializeField, Range(0, 10)] private int _rechargeFactor = 1;
    [SerializeField, Range(0, 10)] private int _rechargeFactorTired = 3;
    private int _currentRechargeFactor = 1;

    public AgentState _agentState;
    private int _rechargeMax = 10;
    private float _time = 0;


    #endregion FIELDS


    #region PROPERTIES
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
    #endregion PROPERTIES

    #region METHODS
    private void Update()
    {
        _time += Time.deltaTime;

        if (_time > _currentRechargeFactor * _energyRechargeTime)
        {
            _time = 0;
            IncreaseEnergy();
        }

        if (_hp > _defenseLevel + 1) _hp = _defenseLevel + 1;
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
        var max = 2 * (EnduranceLevel + 1);
        if (Energy == max) return;

        // Increase the recharge counter.
        _rechargeCounter = System.Math.Min(_rechargeMax, _rechargeCounter + 1);

        // If the recharge counter is at 10, gain an energy cell.
        if (_rechargeCounter < _rechargeMax) return;
        _agentState.IsTired = false;

        _currentRechargeFactor = _rechargeFactor;
        Energy = System.Math.Min(max, Energy + 1);
        _rechargeCounter = 0;
    }
    #endregion METHODS
}
