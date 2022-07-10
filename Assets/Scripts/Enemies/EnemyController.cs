using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _direction;
    private int _state = 1; //0 = roaming, 1 = chasing
    private GameObject _player;
    public Transform _transform;
    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
            if(_player.transform.position.x > _transform.position.x)
            {
                _direction = 1;
            }
            else
            {
                _direction = -1;
            }
        }
    }
}
