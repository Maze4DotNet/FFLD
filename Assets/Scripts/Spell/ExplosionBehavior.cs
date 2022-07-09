using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
     float lifeTime = 0.5f;
     public void Start()
     {
         StartCoroutine(WaitThenDie());
     }
     IEnumerator WaitThenDie()
     {
         yield return new WaitForSeconds(lifeTime);
         Destroy(gameObject);
     }
}
