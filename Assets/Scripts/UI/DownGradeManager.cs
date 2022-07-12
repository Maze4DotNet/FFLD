using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DownGradeManager : MonoBehaviour
{
    private GameObject _player;
    private CharacterSheet _characterSheet;
    public TextMeshProUGUI _atkLevelText;
    public TextMeshProUGUI _dfsLevelText;
    public TextMeshProUGUI _endLevelText;
    public TextMeshProUGUI _magLevelText;
    public TextMeshProUGUI _totalLevel;

    [SerializeField] public bool _special = false;

    private bool _canChoose = true;
    public float _timeToClose = 1000.0f;

    private bool _canClose = false;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterSheet = _player.GetComponent<CharacterSheet>();
        //if (_characterSheet.TotalLevel == 0 && !_special)
        //{
        //    CloseMenuWithoutCheck();
        //}
    }
    void Update()
    {
        _atkLevelText.text = "Attack Level: " + _characterSheet.AttackLevel.ToString();
        _dfsLevelText.text = "Defense Level: " + _characterSheet.DefenseLevel.ToString();
        _endLevelText.text = "Endurance Level: " + _characterSheet.EnduranceLevel.ToString();
        _magLevelText.text = "Magic Level: " + _characterSheet.MagicLevel.ToString();
        string start = "You are level";
        if (_characterSheet.Hp <= 0) start = "You made it down to level";
        _totalLevel.text = $"{start} {_characterSheet.TotalLevel}";
    }
    public void decrAttack()
    {
        if (_characterSheet.AttackLevel > 0 && _canChoose)
        {
            _characterSheet.AttackLevel--;
            _canChoose = false; _canClose = true;
        }
    }
    public void decrDefense()
    {
        if (_characterSheet.DefenseLevel > 0 && _canChoose)
        {
            _characterSheet.Hp = Math.Max(_characterSheet.Hp - 4, 1);
            _characterSheet.DefenseLevel--;
            _canChoose = false; _canClose = true;
        }
    }
    public void decrEndure()
    {
        if (_characterSheet.EnduranceLevel > 0 && _canChoose)
        {
            _characterSheet.EnduranceLevel--;
            _canChoose = false; _canClose = true;
        }
    }
    public void decrMagic()
    {
        if (_characterSheet.MagicLevel > 0 && _canChoose)
        {
            _characterSheet.MagicLevel--;
            _canChoose = false; _canClose = true;
        }
    }

    public void CloseMenu()
    {
        if (_canClose) CloseMenuWithoutCheck();
    }

    public void CloseMenuWithoutCheck()
    {
        Destroy(transform.parent.gameObject);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("kaastest");
    }
}
