using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class MagicMeter : MonoBehaviour
{
    public CharacterSheet _characterSheet;
    public Image _magicMeter;
    public Image _manaValue;

    public Sprite _magicMeterSprite;
    public Sprite _manaValueSprite;
    public Sprite _nothing;


    private void Update()
    {
        if (_characterSheet.MagicLevel == 0)
        {
            _magicMeter.sprite = _nothing;
            _manaValue.sprite = _nothing;
            return;
        }
        _manaValue.transform.localScale = new Vector3(_characterSheet.Mana *3.17f/ 100, 3.17f, 3.17f);
        _magicMeter.sprite = _magicMeterSprite;
        _manaValue.sprite = _manaValueSprite;
    }
}
