using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject _player;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _player = GameObject.Find("Character2");
    }

    // Update is called once per frame
    void Update()
    {
        var pos = _player.transform.position;
        _transform.position = new Vector3(pos.x, pos.y + 2, - 5f);
        //Debug.Log($"\n{_player.transform.position}\n{_transform.position}");
    }
}
