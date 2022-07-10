using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    void Awake(){
        var rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyJump>().JumpAction();
        }
    }
}
