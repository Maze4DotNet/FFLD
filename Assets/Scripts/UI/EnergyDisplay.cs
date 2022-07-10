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
        return 2 * (_characterSheet.EnduranceLevel + 1) - 1;
    }

    internal override int GetValue()
    {
        return _characterSheet.Energy;
    }
}

