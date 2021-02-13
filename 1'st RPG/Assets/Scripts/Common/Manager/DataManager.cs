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


        public Dictionary<int, List<QuestData>> questDict;   //key : npc id, Value: 해당되는 퀘스트들
        public List<QuestData> questDatabase;

        private void Awake()
        {
            questDict = new Dictionary<int, List<QuestData>>();
        }
        void Start()
        {
            foreach (QuestData data in questDatabase)
            {
                AddQuestData(data);
            }
        }

        void Update()
        {
            
        }

        public void AddQuestData(QuestData data)
        {
            
            if (!questDict.ContainsKey(data.client))
                questDict[data.client] = new List<QuestData>();

            questDict[data.client].Add(Instantiate<QuestData>(data));
            
            if(data.isActive)
                GameObject.Find("Environment").transform.Find("Npcs").Find(data.client.ToString()).GetComponent<Npc>().SetQuestImage(1);
            
        }
    }
}