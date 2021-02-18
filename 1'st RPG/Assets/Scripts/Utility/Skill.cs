using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected int _atk;
    protected Transform _caster;
    protected string _target;

    public void SetAtk(int atk)
    {
        _atk = atk;
    }
    public void SetCaster(Transform caster)
    {
        _caster = caster;
    }
    public void SetTarget(string target)
    {
        _target = target;
    }
    
}
