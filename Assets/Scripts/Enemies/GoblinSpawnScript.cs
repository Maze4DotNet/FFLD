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
    public bool _goblorIsHere = false;
    public int _nrOfGoblinsWhenGoblorCame;
    public bool _currentlySpawning = false;

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
        SpawnPickup();
    }

    private Vector2 GetRandomSpawn()
    {
        var index = _random.Next(_spawns.Count);
        var spawn = _spawns[index];
        return spawn;
    }

    private void SpawnPickup()
    {
        var spawn = GetRandomSpawn();
        SpawnPickup(spawn);
        StartCoroutine(WaitThenSpawn());
    }

    private void SpawnPickup(Vector2 position)
    {
        GameObject obj = _random.Next() % 3 == 0 ? _energy : _heart;

        GameObject spawnedItem = Instantiate(obj, position, Quaternion.identity);
        StartCoroutine(WaitThenDespawn(spawnedItem));
    }

    IEnumerator WaitThenDespawn(GameObject spawnedItem)
    {
        yield return new WaitForSeconds(20f);
        Destroy(spawnedItem);
    }

    private void FixedUpdate()
    {
        var reverseLevel = 20 - _characterSheet.TotalLevel;

        if (_goblorIsHere) return;
        if (_nrOfGoblins < 4 && !_currentlySpawning)
        {
            _nrOfGoblins++;
            _currentlySpawning = true;
            Invoke("AlmostSpawnGoblin", _respawnTimer);
        }
        if (!_goblorIsHere && _characterSheet.TotalLevel == 0)
        {
            // Spawn Goblor, king of the goblins.
            SpawnGoblin(4);
            _goblorIsHere = true;
            _nrOfGoblinsWhenGoblorCame = _nrOfGoblins;
        }
    }

    private void AlmostSpawnGoblin()
    {
        int toughness = 1;
        if (_goblinsEverSpawned != 0 && _goblinsEverSpawned % 5 == 0) toughness++;
        if (_goblinsEverSpawned != 0 && _goblinsEverSpawned % 10 == 0) toughness++;
        SpawnGoblin(toughness);
        _currentlySpawning = false;
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

    internal void Died(Vector2 position, int damage)
    {
        _nrOfGoblins--;
        var roll = _random.Next(6);
        if (roll == 0 || damage > 1) SpawnPickup(position);
    }
}
