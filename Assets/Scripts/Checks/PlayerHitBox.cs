using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PlayerHitBox : MonoBehaviour
{
    public CharacterSheet _characterSheet;
    public AgentState _agentState;
    SoundManager _soundManager;

    public void Awake()
    {
        _soundManager = gameObject.GetComponent<SoundManager>();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var otherObject = collision.gameObject;
    //    if (!otherObject.name.Contains("Goblin")) return;

    //    int damage;
    //    if (otherObject.name.Contains("Weapon")) damage = otherObject.GetComponent<GoblinWeaponScript>()._damage;
    //    else damage = otherObject.GetComponent<GoblinType>()._damage;

    //    _characterSheet.TakeDamage(otherObject, damage);
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        var otherObject = collision.gameObject;
        if (otherObject.name.Contains("Heart"))
        {
            if (_characterSheet.Hp == _characterSheet.MaxHP) return;
            _characterSheet.Heal();
            Destroy(otherObject);
            _soundManager.PlaySound("heal");
        }
        else if (otherObject.name.Contains("Energy"))
        {
            if (_characterSheet.Energy == _characterSheet.MaxEnergy) return;
            _characterSheet.RestoreEnergy();
            Destroy(otherObject);
            _soundManager.PlaySound("energy");
        }
    }
}
