using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{

    [SerializeField]
    string _name;
    [SerializeField]
    Sprite _sprite;
    [SerializeField]
    int _category;
    [SerializeField]
    int _price;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public int Category => _category;
    public int Price => _price;
}

