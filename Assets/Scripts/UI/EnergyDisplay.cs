using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class EnergyDisplay : ResourceDisplay
{
    internal override int GetLevel()
    {
        return _characterSheet.MaxEnergy-1;
    }

    internal override int GetValue()
    {
        return _characterSheet.Energy;
    }
}

