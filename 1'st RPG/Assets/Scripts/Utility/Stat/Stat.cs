using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    string _name;
    [SerializeField]
    int _maxHp;


    [SerializeField]
    int _level;
    [SerializeField]
    float _maxExp;
    
    [SerializeField]
    int _atk;
    [SerializeField]
    int _def;

    [SerializeField]
    float _moveSpeed;

    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Hp { get; set; }
    public int Level { get { return _level; } set { _level = value; } }
    public string Name { get { return _name; } set { _name = value; } }

    public float MaxExp { get { return _maxExp; } set { _maxExp = value; } }
    public float Exp { get; set; }

    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }


    void Start()
    {
        Hp = _maxHp;
    }

    public virtual void ResetStat()
    {
        Hp = _maxHp;
    }
    public void Heal(int amount)
    {
        Hp = Mathf.Clamp(amount+Hp,0,MaxHp);
    }

    public virtual void AddExp(float exp)
    {
        
    }
}
