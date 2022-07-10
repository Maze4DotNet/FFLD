using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _direction;
    private int _state = 1; //0 = roaming, 1 = chasing
    public GameObject _player;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == 0)
        {
            _direction = 0;
        }
        else if (_state == 1)
        {
            _direction = -1;
        }
    }
}
