using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameScene.Skill;
namespace common
{
    public class DataManager : Singleton<DataManager>
    {
        public List<SkillDatabase> skillDatabases;
        public List<StatusData> statusDatabase;
        void Start()
        {
            
        }

        void Update()
        {

        }
    }
}