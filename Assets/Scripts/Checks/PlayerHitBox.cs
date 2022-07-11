﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PlayerHitBox : MonoBehaviour
{
    public CharacterSheet _characterSheet;
    public AgentState _agentState;


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var otherObject = collision.gameObject;
    //    if (!otherObject.name.Contains("Goblin")) return;

    //    int damage;
    //    if (otherObject.name.Contains("Weapon")) damage = otherObject.GetComponent<GoblinWeaponScript>()._damage;
    //    else damage = otherObject.GetComponent<GoblinType>()._damage;

    //    _characterSheet.TakeDamage(otherObject, damage);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherObject = collision.gameObject;
        if (otherObject.name.Contains("Heart"))
        {
            _characterSheet.Heal();
        Destroy(otherObject);
        }
        else if (otherObject.name.Contains("Energy"))
            {
                _characterSheet.RestoreEnergy();
                Destroy(otherObject);
            }
    }
}
