using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class HeartsDisplay : ResourceDisplay
{
    internal override int GetLevel()
    {
        return _characterSheet.MaxHP-1;
    }

    internal override int GetValue()
    {
        return _characterSheet.Hp;
    }
}

