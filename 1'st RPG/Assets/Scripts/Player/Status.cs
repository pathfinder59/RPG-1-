using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace common
{
    public class Status
    {
        string _name;
        int _maxHp;
        int _hp;
        int _level;

        int _exp;
        int _maxExp;

        public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
        public int Hp { get { return _hp; } set { _hp = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        public int MaxExp { get { return _maxExp; } set { _maxExp = value; } }
        public int Exp { get { return _exp; } set { _exp = value; } }
    }
}