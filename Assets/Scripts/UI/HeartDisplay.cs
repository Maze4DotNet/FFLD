using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class HeartDisplay : MonoBehaviour
{
    public CharacterSheet _characterSheet;

    public Image[] _hearts;
    public Sprite _fullHeart;
    public Sprite _emptyHeart;
    public Sprite _nothing;

    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            if (_characterSheet.DefenseLevel < i) _hearts[i].sprite = _nothing;
            else if (_characterSheet.Hp > i) _hearts[i].sprite= _fullHeart;
            else _hearts[i].sprite= _emptyHeart;
        }
    }
}

