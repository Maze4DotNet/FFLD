using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DownGradeManager : MonoBehaviour
{
    private GameObject _player;
    private CharacterSheet _characterSheet;
    public TextMeshProUGUI _atkLevelText;
    public TextMeshProUGUI _dfsLevelText;
    public TextMeshProUGUI _endLevelText;
    public TextMeshProUGUI _magLevelText;
    private bool _canChoose = true;
    public float _timeToClose = 0.0f;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterSheet = _player.GetComponent<CharacterSheet>();
    }
    void Update()
    {
        _atkLevelText.text = "Attack Level: " + _characterSheet.AttackLevel.ToString();
        _dfsLevelText.text = "Defense Level: " + _characterSheet.DefenseLevel.ToString();
        _endLevelText.text = "Endurance Level: " + _characterSheet.EnduranceLevel.ToString();
        _magLevelText.text = "Magic Level: " + _characterSheet.MagicLevel.ToString();
    }
    public void decrAttack()
    {
        if(_characterSheet.AttackLevel > 0 && _canChoose)
        {
            _characterSheet.AttackLevel --;
            _canChoose = false;
            Invoke("CloseMenu", _timeToClose);
        }
    }
    public void decrDefense()
    {
        if(_characterSheet.DefenseLevel > 0 && _canChoose)
        {
            _characterSheet.DefenseLevel --;
            _canChoose = false;
            Invoke("CloseMenu", _timeToClose);
        }
    }
    public void decrEndure()
    {
        if(_characterSheet.EnduranceLevel > 0 && _canChoose)
        {
            _characterSheet.EnduranceLevel --;
            _canChoose = false;
            Invoke("CloseMenu", _timeToClose);
        }
    }
    public void decrMagic()
    {
        if(_characterSheet.MagicLevel > 0 && _canChoose)
        {
            _characterSheet.MagicLevel --;
            _canChoose = false;
            Invoke("CloseMenu", _timeToClose);
        }
    }
    void CloseMenu(){
        Destroy(transform.parent.gameObject);
    }
}
