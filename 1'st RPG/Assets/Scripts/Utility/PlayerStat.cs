using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;

using common;
using System.Linq;

public class PlayerStat : Stat
{
    [SerializeField]
    string _classType;

    [SerializeField]
    int hpRisingValue;

    [SerializeField]
    int atkRisingValue;

    int _skillPoint;


    IDictionary<string, int> skillLevels;


    void Update()
    {
        
            
    }

    public void SetSkillLevels()
    {
        var databases = GameObject.Find("DataManager").GetComponent<DataManager>().skillDatabases;
        skillLevels = new Dictionary<string, int>();

        foreach (var skillData in databases[0].SkillList)
            skillLevels.Add(skillData.Name, 0);

        SkillDatabase database = databases.FirstOrDefault(c => c.ClassName == _classType) ?? null;
        if (database)
        {
            foreach (var skillData in database.SkillList)
                skillLevels.Add(skillData.Name, 0);
        }
    }
    public IDictionary<string, int> SkillLevels { get { return skillLevels; } }

    public string ClassType {get{return _classType; } set { _classType = value; } }
    public int SkillPoint { get { return _skillPoint; } set { _skillPoint = value; } }

    public override bool AddExp(float exp)
    {
        if (Level != 10) //만렙 아닐경우
        {
            Exp += exp;
            if (Exp >= MaxExp)
            {
                Level += 1;
                Exp = Exp - MaxExp;
                //MaxExp증가
                MaxExp = Mathf.Floor(MaxExp * 1.5f);
                Atk += atkRisingValue;
                MaxHp += hpRisingValue;

                SkillPoint++;
                return true;
            }
        }
        return false;
    }

}
