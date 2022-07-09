using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    public int _direction;
    public GameObject _explosion;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(_direction, 0, 0.1f*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy(){
        Instantiate(_explosion, this.transform.position, Quaternion.identity);
    }
}
