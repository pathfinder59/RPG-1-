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


        public Dictionary<int, List<QuestData>> questList;   //key : npc id, Value: 해당되는 퀘스트들
        public List<QuestData> questDatabase;

        private void Awake()
        {
            questList = new Dictionary<int, List<QuestData>>();
        }
        void Start()
        {
            foreach (QuestData data in DataManager.Instance.questDatabase)
            {
                if (!questList.ContainsKey(data.client))
                    questList[data.client] = new List<QuestData>();
                questList[data.client].Add(data);
            }
        }

        void Update()
        {

        }
    }
}