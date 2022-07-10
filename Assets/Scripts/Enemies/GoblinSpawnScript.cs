using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GoblinSpawnScript : MonoBehaviour
{
    public int _nrOfGoblins = 0;
    public GameObject _goblinPrefab;
    public System.Random _random = new System.Random();
    public CharacterSheet _characterSheet;
    public int _goblinsEverSpawned = 0;

    public List<Vector2> _spawns = new List<Vector2>()
    {
        new Vector2(10,5),
        new Vector2(-10,5),
        new Vector2(0,5),
        new Vector2(0,0),
        new Vector2(3,0),
        new Vector2(-3,0),
        new Vector2(7,-3),
        new Vector2(-7,-3),
        new Vector2(10,-5),
        new Vector2(-10,-5),
        new Vector2(-10,0)
    };

    private void FixedUpdate()
    {
        var reverseLevel = 20 - _characterSheet.TotalLevel;

        if (_nrOfGoblins < reverseLevel + 1)
        {
            int toughness = 1;
            if (_goblinsEverSpawned!=0&&_goblinsEverSpawned % 5 == 0) toughness++; 
            if (_goblinsEverSpawned!=0&&_goblinsEverSpawned % 10 == 0) toughness++;
            SpawnGoblin(toughness);
        }
    }

    private void SpawnGoblin(int toughness)
    {
        var index = _random.Next(_spawns.Count);
        var spawn = _spawns[index];
        GameObject newGoblin = Instantiate(_goblinPrefab, spawn, Quaternion.identity);
        GoblinType type = newGoblin.GetComponent<GoblinType>();
        type.WhosYourDaddy(this, toughness);
        _nrOfGoblins++;
        _goblinsEverSpawned++;
    }

    internal void Died()
    {
        _nrOfGoblins--;
    }
}
