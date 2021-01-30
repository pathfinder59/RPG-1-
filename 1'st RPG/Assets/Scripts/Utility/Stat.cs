using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    string _name;
    [SerializeField]
    int _maxHp;
    int _hp;
    [SerializeField]
    int _level;
    [SerializeField]
    float _maxExp;
    float _exp;
    [SerializeField]
    int _atk;
    [SerializeField]
    float _moveSpeed;

    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int Level { get { return _level; } set { _level = value; } }
    public string Name { get { return _name; } set { _name = value; } }

    public float MaxExp { get { return _maxExp; } set { _maxExp = value; } }
    public float Exp { get { return _exp; } set { _exp = value; } }

    public int Atk { get { return _atk; } set { _atk = value; } }

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }


    void Start()
    {
        _hp = _maxHp;
    }

    public virtual void ResetStat()
    {
        _hp = _maxHp;
    }
    public virtual bool AddExp(float exp)
    {
        return false;
    }
}
