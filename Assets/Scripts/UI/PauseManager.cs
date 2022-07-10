using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
