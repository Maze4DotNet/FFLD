using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    public int _direction;
    public GameObject _explosion;
    public CharacterSheet _characterSheet;
    public Rigidbody2D _body;
    [SerializeField, Range(0f, 100f)] public float _speedX = 100f;
    [SerializeField, Range(0f, 100f)] public float _speedY = 100f;
    [SerializeField, Range(0f, 100f)] public float _fraction = 1.5f;


    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void DieEneNaAwake()
    {
        _body.velocity = new Vector2(_direction * _speedX, 1f * _speedY);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(_direction, 0, 0.1f*Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Contains("Trigger")) return;
        if (other.name.Contains("Sword")) return;
        if (other.name.Contains("Weapon")) return;
        if (other.name.Contains("Heart")) return; 
        if (other.name.Contains("Energy")) return;
        if (other.name == "Goblin")
        {
            return;
        }

        if (other.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy(){
        GameObject explosion = Instantiate(_explosion, this.transform.position, Quaternion.identity);
        var scale = explosion.transform.localScale;
        explosion.transform.localScale = new Vector3(scale.x * _characterSheet.MagicLevel/ _fraction, scale.y * _characterSheet.MagicLevel/_fraction, scale.z);
    }
}
