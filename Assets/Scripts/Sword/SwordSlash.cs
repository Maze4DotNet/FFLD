using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFLD
{
    internal class SwordSlash : EnergyConsumingAction
    {

        private void Awake()
        {
            _actionName = "Slash";
            _action = () => PerformSlash();
        }

        private void PerformSlash()
        {
            
        }
    }
}
