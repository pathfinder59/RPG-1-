using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
using GameScene.Skill;
using System.Linq;

public class PlayerStatus : Status
{

    string _classType;
    int _skillPoint;


    IDictionary<string, int> skillLevels;
    
    public void SetSkillLevels()
    {
        var databases = GameObject.Find("DataManager").GetComponent<DataManager>().skillDatabases;
        skillLevels = new Dictionary<string, int>();

        foreach (var skillData in databases[0].SkillList)
            skillLevels.Add(skillData._name, 0);

        SkillDatabase database = databases.FirstOrDefault(c => c.ClassName == _classType) ?? null;
        if (database)
        {
            foreach (var skillData in database.SkillList)
                skillLevels.Add(skillData._name, 0);
        }
    }
    public IDictionary<string, int> SkillLevels { get { return skillLevels; } }

    int _passiveLevel;
    int _activeLevel;
    int _healLevel;

    public string ClassType {get{return _classType; } set { _classType = value; } }
    public int SkillPoint { get { return _skillPoint; } set { _skillPoint = value; } }

    public int PassiveLevel { get { return _passiveLevel; } set { _passiveLevel = value; } }
    public int ActiveLevel { get { return _activeLevel; } set { _activeLevel = value; } }
    public int HealLevel { get { return _healLevel; } set { _healLevel = value; } }
}
