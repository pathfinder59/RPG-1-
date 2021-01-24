using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameScene.Skill
{
    [CreateAssetMenu(fileName = "SkillDatabase",menuName = "Scriptable Object/SkillDataBase")]
    public class SkillDatabase : ScriptableObject
    {
        [SerializeField]
        string className;
        [SerializeField]
        List<SkillData> skillList;

        public string ClassName => className;
        public List<SkillData> SkillList => skillList;
    }
}