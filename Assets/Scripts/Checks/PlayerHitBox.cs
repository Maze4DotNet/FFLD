using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PlayerHitBox:MonoBehaviour
{
    public CharacterSheet _characterSheet;
    public AgentState _agentState;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionHandler(collision);
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        CollisionHandler(collision);
    }

    private void CollisionHandler(Collision2D collision)
    {
        var otherObject = collision.gameObject;
        if (!otherObject.name.Contains("Goblin")) return;
        _characterSheet.TakeDamage(otherObject);

    }
}
