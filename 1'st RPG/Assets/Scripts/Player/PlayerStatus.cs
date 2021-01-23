using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class PlayerStatus : Status
{

    int _skillPoint;
    int _passiveLevel;
    int _activeLevel;
    int _healLevel;

    public int SkillPoint { get { return _skillPoint; } set { _skillPoint = value; } }

    public int PassiveLevel { get { return _passiveLevel; } set { _passiveLevel = value; } }
    public int ActiveLevel { get { return _activeLevel; } set { _activeLevel = value; } }
    public int HealLevel { get { return _healLevel; } set { _healLevel = value; } }
}
