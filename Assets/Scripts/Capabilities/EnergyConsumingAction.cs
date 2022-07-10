using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EnergyConsumingAction : MonoBehaviour
{
    internal string _actionName;
    internal Action _action;

    public CharacterSheet _characterSheet;
    public AgentState _agentState;
    private bool _desiredAction = false;

    internal bool _successful = true;

    internal void Update()
    {
        _desiredAction |= Input.GetButtonDown(_actionName);
    }

    internal void FixedUpdate()
    {
        if (!_desiredAction) return;
        _desiredAction = false;
        if (_agentState.CantMove) return;
        PerformAction();
    }

    virtual internal void PerformAction()
    {
        Debug.Log(
            $"Action: {_actionName}\n" +
            $"Energy: {_characterSheet.Energy}");

        if (_characterSheet.Energy == 0)
        {
            Debug.Log("No energy to perform action!");
            return;
        }
        _action.Invoke();
        _characterSheet.DecreaseEnergy(_actionName, _successful);
        _successful = true;
    }
}
