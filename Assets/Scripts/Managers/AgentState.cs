using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFLD
{
    public class AgentState : MonoBehaviour
    {
        [SerializeField] private int _facingDirection = 1;
        private bool _isDashing = false;
        public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
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
    }
}
