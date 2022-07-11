using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearCrown : MonoBehaviour
{
    public bool _shouldWear = false;
    // Start is called before the first frame update
    void Awake()
    {
        if(!_shouldWear)
            Destroy(gameObject);
    }
}
