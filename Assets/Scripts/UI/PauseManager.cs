using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject _menuUI;
    // Start is called before the first frame update
    void Awake()
    {
        _menuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _menuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
