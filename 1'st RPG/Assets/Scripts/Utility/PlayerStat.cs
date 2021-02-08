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

    float sumExp = 0f;
    
    IDictionary<string, int> skillLevels;


    void Update()
    {
        
            
    }
    void LateUpdate()
    {
        CalculateExp();
        sumExp = 0f;
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

    public override void AddExp(float exp)
    {
        if (Level == 10) //만렙 아닐경우
            return;

        sumExp += exp;
        return;
    }

    void CalculateExp()
    {
        if (sumExp == 0f)
            return;
        Exp += sumExp;
        var popupText = ParticlePoolManager.Instance.Spawn("PopUpText", transform.position + new Vector3(0, 3, 0));
        popupText.GetComponentInChildren<TextMesh>().text = "+" + sumExp.ToString();
        popupText.GetComponentInChildren<TextMesh>().color = new Color(0, 1, 0, 1);
        if (Exp >= MaxExp)
        {
            Level += 1;
            Exp = Exp - MaxExp;
            //MaxExp증가
            MaxExp = Mathf.Floor(MaxExp * 1.5f);
            Atk += atkRisingValue;
            MaxHp += hpRisingValue;

            SkillPoint++;

            var levelUpParticle = ParticlePoolManager.Instance.Spawn("LevelUp");
            levelUpParticle.transform.position = transform.position;
            levelUpParticle.GetComponent<ParticleTime>().SetTarget(transform);
            EventManager.Emit("UpdateStatus");
            return;
        }
    }
    
}
