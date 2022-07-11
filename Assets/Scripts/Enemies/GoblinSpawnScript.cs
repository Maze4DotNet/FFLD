using System;
using System.Collections;
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
    [SerializeField, Range(0, 100f)] private float _respawnTimer = 0.1f;

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

    [SerializeField, Range(0f, 100f)] private float _heartSpawnTime;
    [SerializeField, Range(0f, 100f)] private float _initialEnergySpawnerTimer = 5f;
    public GameObject _heart;
    public GameObject _energy;


    private void Awake()
    {
        StartCoroutine(WaitThenSpawn());
    }

    IEnumerator WaitThenSpawn()
    {
        yield return new WaitForSeconds(_heartSpawnTime);
        Spawn();
    }

    private Vector2 GetRandomSpawn()
    {
        var index = _random.Next(_spawns.Count);
        var spawn = _spawns[index];
        return spawn;
    }

    private void Spawn()
    {
        var spawn = GetRandomSpawn();
        GameObject obj = _random.Next() % 3 == 0 ? _energy : _heart;

        Instantiate(obj, spawn, Quaternion.identity);
        StartCoroutine(WaitThenSpawn());
    }

    private void FixedUpdate()
    {
        var reverseLevel = 20 - _characterSheet.TotalLevel;

        if (_nrOfGoblins < Math.Max(1, reverseLevel / 2))
        {
            _nrOfGoblins++;
            Invoke("AlmostSpawnGoblin", _respawnTimer);
        }
    }

    private void AlmostSpawnGoblin()
    {
        int toughness = 1;
        if (_goblinsEverSpawned != 0 && _goblinsEverSpawned % 5 == 0) toughness++;
        if (_goblinsEverSpawned != 0 && _goblinsEverSpawned % 10 == 0) toughness++;
        SpawnGoblin(toughness);
    }

    private void SpawnGoblin(int toughness)
    {
        Vector2 spawn;
        bool close;
        float distance;
        do
        {
            spawn = GetRandomSpawn();
            distance = Vector2.Distance(spawn, transform.position);
            close = distance < 5;
        } while (close);
        GameObject newGoblin = Instantiate(_goblinPrefab, spawn, Quaternion.identity);
        GoblinType type = newGoblin.GetComponent<GoblinType>();
        type.WhosYourDaddy(this, toughness);
        _goblinsEverSpawned++;
    }

    internal void Died()
    {
        _nrOfGoblins--;
    }
}
