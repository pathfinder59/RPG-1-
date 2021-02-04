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
        public List<DialogData> dialogDatabase;
        void Start()
        {
            
        }

        void Update()
        {

        }
    }
}