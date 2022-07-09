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

        public bool IsDashing { get; set; }
        public bool IsAirborne { get; set; }
        public bool IsAttacking { get; set; }
        public bool IsWalking { get; set; }
        public bool IsTakingDamage { get; set; }
        public bool IsDead { get; set; }
        public bool IsCelebrating { get; set; }
        public bool IsTired { get; set; }

        Animator animator;
         // Use this for initialization
        void Start () {
            animator = GetComponent<Animator>();
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
        private void Update(){
            animator.SetBool("isWalking", IsWalking);
            animator.SetBool("isDashing", IsDashing);
            animator.SetBool("isAiborne", IsAirborne);
            animator.SetBool("isAttacking", IsAttacking);
            animator.SetBool("isDamaged", IsTakingDamage);
            animator.SetBool("isDead", IsDead);
            animator.SetBool("isCelebrating", IsCelebrating);
            animator.SetBool("isTired", IsTired);
        }
    }
}
