using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinType : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] public float _size;
    [SerializeField, Range(0, 100)] public int _hp;
    [SerializeField, Range(0, 6)] public int _damage;

    /// <summary>
    /// 0 for dagger, 1 for sword.
    /// </summary>
    [SerializeField, Range(0, 6)] public int _weaponType;

    public AgentState _agentState;

    public GameObject _weapon;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        var pos = this.transform.position + new Vector3(_agentState.FacingDirection * 0.5f, 0);
        GameObject newWeapon = Instantiate(_weapon, pos, Quaternion.identity);

        GoblinWeaponScript weaponScript = newWeapon.GetComponent<GoblinWeaponScript>();
        AgentState agentState = GetComponent<AgentState>();
        weaponScript.Initialize(gameObject, this, _weaponType, agentState.FacingDirection);
        weaponScript.Attack();
    }
}
