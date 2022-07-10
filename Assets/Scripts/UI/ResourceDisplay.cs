using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal abstract class ResourceDisplay : MonoBehaviour
{
    public CharacterSheet _characterSheet;

    public Image[] _resources;
    public Sprite _fullResource;
    public Sprite _emptyResource;
    public Sprite _nothing;

    internal abstract int GetLevel();
    internal abstract int GetValue();

    void Update()
    {
        for (int i = 0; i < _resources.Length; i++)
        {
            if (GetLevel() < i) _resources[i].sprite = _nothing;
            else if (GetValue() > i) _resources[i].sprite = _fullResource;
            else _resources[i].sprite = _emptyResource;
        }
    }
}

