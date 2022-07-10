using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DownGradeManager : MonoBehaviour
{
    public GameObject _player;
    private CharacterSheet _characterSheet;
    public TextMeshProUGUI _atkLevelText;
    public TextMeshProUGUI _dfsLevelText;
    public TextMeshProUGUI _endLevelText;
    public TextMeshProUGUI _magLevelText;
    private bool _canChoose = true;

    void Awake()
    {
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
        if(_characterSheet.AttackLevel > 0)
            {
                _characterSheet.AttackLevel --;
                _canChoose = false;
            }
    }
    public void decrDefense()
    {
        if(_characterSheet.DefenseLevel > 0)
        {
            _characterSheet.DefenseLevel --;
            _canChoose = false;
        }
    }
    public void decrEndure()
    {
        if(_characterSheet.EnduranceLevel > 0)
        {
            _characterSheet.EnduranceLevel --;
            _canChoose = false;
        }
    }
    public void decrMagic()
    {
        if(_characterSheet.MagicLevel > 0)
        {
            _characterSheet.MagicLevel --;
            _canChoose = false;
        }
    }
}
